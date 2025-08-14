using System.Collections;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3;
using Fusion;
using UnityEngine;
using UnityEngine.AI;

public class NPCNetwork2 : NetworkBehaviour
{
   [Header("Player Tagert")]
   public GameObject[] playerTagert;
    [Header("NavMesh Agent")]
    public NavMeshAgent navMeshAgent;
    [Header("NPC Animator")]
    public Animator npcAnimator;
    [Header("NPC Old Transform")]
    public Vector3 npcOldTransform;
    [Header("Attack Event")]
    private float fireCooldown = 1f; // thời gian giữa các phát bắn (giây)
    private float fireTime = 0f;
    public float distanceToPlayer = 20f;
    [Header("Old rotation")]
    public Vector3 oldRotation;

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcSetAnimatorRunNPC(float i)
    {
        npcAnimator.SetFloat("NPCRun", i);
        Debug.Log("Set Animator Run NPC");
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcSetAnimatorAttackNPC()
    {
        npcAnimator.SetTrigger("HandelAttack");
        Debug.Log("Set Animator Attack NPC");
    }
    private void Start()
    {
        npcOldTransform = transform.position;
        oldRotation = transform.rotation.eulerAngles;
    }
    public override void FixedUpdateNetwork()
    {
        playerTagert = GameObject.FindGameObjectsWithTag("Human");
    
        
           
            // Chỉ thực hiện logic nếu có quyền sở hữu trạng thái
            for (int i = 0; i < playerTagert.Length; i++)
            {
                if (playerTagert[i] != null)
                {
                    if(Vector3.Distance(playerTagert[i].transform.position, transform.position) <=distanceToPlayer)
                    {navMeshAgent.speed = 7f; 
                        npcAnimator.speed = 1.3f;
                        navMeshAgent.SetDestination(playerTagert[i].transform.position);
                      RpcSetAnimatorRunNPC(navMeshAgent.velocity.magnitude);
                    //Debug.Log("NPC dang di chuyển đến người chơi: " + playerTagert[i].name);
                    if (Vector3.Distance(playerTagert[i].transform.position, transform.position) <= 4.5f)
                        {
                            Debug.Log($"distance: {Vector3.Distance(playerTagert[i].transform.position, transform.position)}");
                            navMeshAgent.speed = 0f;
                            npcAnimator.SetFloat("NPCRun", 0f);
                           // Debug.Log("NPC đã đến gần người chơi: " + playerTagert[i].name);
                            fireTime += Runner.DeltaTime;
                            if (fireTime >= fireCooldown){
                                Vector3 direction = (playerTagert[i].transform.position - transform.position).normalized;
                                transform.rotation = Quaternion.LookRotation(direction);
                              RpcSetAnimatorAttackNPC();

                            fireTime = 0f; // Reset thời gian sau khi bắn
}


                            }
                     
                    }
                    else
                    {
                        
                        navMeshAgent.speed = 2f;
                        npcAnimator.speed = 1f;
                       
                        navMeshAgent.SetDestination(npcOldTransform);
                    RpcSetAnimatorRunNPC(navMeshAgent.velocity.magnitude);
                   
                    // Debug.Log("NPC dang di chuyen ve vi tri cu: " + npcOldTransform);
                }
                if (transform.position == npcOldTransform)
                {
                    transform.rotation = Quaternion.Euler(oldRotation);
                }

            }
            }
        }
    public void SetDistancePlayer(float i)
    {
                distanceToPlayer = i;
    }
    }
    

