using System.Collections;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3;
using Fusion;
using UnityEngine;
using UnityEngine.AI;

public class NPCNetworkBoss : NetworkBehaviour
{
    [Header("Player Tagert")]
    public GameObject[] playerTagert;
    [Header("NavMesh Agent")]
    public NavMeshAgent navMeshAgent;
    [Header("NPC Animator")]
    public Animator npcAnimator;
    [Header("NPC Old Transform")]
    public Vector3 npcOldTransform;
    [Header("Transform Fire Attack")]
    public Transform fireAttackTransform;
    [Header("GameOject Fire Attack")]
    public GameObject fireAttackGameObject;
    [Header("Attack Event")]
    private float fireCooldown = 5f; // thời gian giữa các phát bắn (giây)
    private float fireTime = 0f;
    [Header("List NPC")]
  
    public GameObject[] npcAD;
    public GameObject[] npcWarrior;
    public bool startAttack = true;


    private void Update()
    {
        npcAD = GameObject.FindGameObjectsWithTag("NPCAD");
        npcWarrior = GameObject.FindGameObjectsWithTag("NPCWarrior");
    }
   
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcSetAnimatorRunNPC(float i)
    {
        npcAnimator.SetFloat("NPCRun", i);
        Debug.Log("Set Animator Run NPC");
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcSetAnimatorAttackNPC()
    {
        npcAnimator.SetTrigger("FireAttack");
       
        NetworkObject fire = Runner.Spawn(fireAttackGameObject, fireAttackTransform.position, Quaternion.identity);
        Rigidbody rigidbody = fire.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddForce(fireAttackTransform.forward * 30f, ForceMode.Impulse);
        }
        StartCoroutine(DestroyFireBall(fire));
        Debug.Log("Set Animator Attack NPC"); }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcStartAttackAnimator()
    {
                npcAnimator.SetTrigger("StartAttack");
        Debug.Log("Start Attack Animator");
    }

    private void Start()
    {
        npcOldTransform = transform.position;
    }
    public override void FixedUpdateNetwork()
    {
        playerTagert = GameObject.FindGameObjectsWithTag("Human");

        



        // Chỉ thực hiện logic nếu có quyền sở hữu trạng thái
        for (int i = 0; i < playerTagert.Length; i++)
        {
            if (playerTagert[i] != null)
            {
               
                    if (Vector3.Distance(playerTagert[i].transform.position, transform.position) <= 30f)
                    {
                    if (startAttack)
                    {
                        RpcStartAttackAnimator();
                        startAttack = false;
                    }
                    fireTime += Runner.DeltaTime;
                    
                   
                    Vector3 dis  = playerTagert[i].transform.position - transform.position;
                    transform.rotation = Quaternion.LookRotation(dis);
                    for (int j = 0; j < npcAD.Length;  j++)
                    {
                            npcAD[j].GetComponent<NPCNetwork1>().SetDistancePlayer(200f);
                       
                        
                    }
                    for (int j = 0; j < npcWarrior.Length; j++)
                    {
                       
                      
                            npcWarrior[j].GetComponent<NPCNetwork2>().SetDistancePlayer(200f);
                        
                    }


                        //  Debug.Log("NPC đã đến gần người chơi: " + playerTagert[i].name);
                        fireTime += Runner.DeltaTime;
                        if (fireTime >= fireCooldown)
                        {
                            Vector3 direction = (playerTagert[i].transform.position - transform.position).normalized;
                            fireAttackTransform.rotation = Quaternion.LookRotation(direction);
                            transform.rotation = Quaternion.LookRotation(direction);
                            RpcSetAnimatorAttackNPC();
                            fireTime = 0f; // Reset thời gian sau khi bắn
                        }


                    }
                else if (Vector3.Distance(playerTagert[i].transform.position, transform.position) > 30f)
                {
                    for (int j = 0; j < npcAD.Length; j++)
                    {
                        
                        
                            npcAD[j].GetComponent<NPCNetwork1>().SetDistancePlayer(20f);
                        
                       

                    }
                    for (int j = 0; j < npcWarrior.Length; j++)
                    {
                        
                        
                            npcWarrior[j].GetComponent<NPCNetwork2>().SetDistancePlayer(20f);
                        
                        
                    }
                    startAttack = true;
                    transform.rotation = Quaternion.Euler(0f, npcOldTransform.y-90, 0f);


                }
                
                    
                   
                }

            }
        
    } 

    public IEnumerator DestroyFireBall(NetworkObject b)
    {
        yield return new WaitForSeconds(2.5f);
        Runner.Despawn(b);
    }
}
