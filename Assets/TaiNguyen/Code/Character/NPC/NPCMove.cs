using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public GameObject tager;
    public Vector3 viTriBanDau;
    [SerializeField] Animator animator;
    public bool nhanNV = true;
    [SerializeField]  public GameObject hoiThoai;
    //[SerializeField] public NV NV;
    public bool quayVeChoCu = false;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private CinemachineFollow cinemachineFollow;
        [SerializeField] private TextMeshProUGUI TextMeshProUGUI;



    void Start()
    {
        viTriBanDau = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        var xetVitri = Vector3.Distance(tager.transform.position, viTriBanDau);
        if (xetVitri <= 5 &&   quayVeChoCu == false)
        { var viTriDung = new Vector3(tager.transform.position.x , tager.transform.position.y,tager.transform.position.z - 1);
            agent.SetDestination(viTriDung);
           
            xetVitri = Vector3.Distance(tager.transform.position, transform.position);
            if (xetVitri <= 2 && nhanNV == true)
            {
               
                hoiThoai.SetActive(true);
                if (Input.GetKeyDown(KeyCode.K)){
                   // NV.HienNV();
                    hoiThoai.SetActive(false);
                    nhanNV = false;
                    quayVeChoCu = true;
                    AudioSource.Play();
                }
            }
        }
        else
        {
            agent.SetDestination(viTriBanDau); 
            hoiThoai.SetActive(false) ;
            
        }
        animator.SetFloat("Move",agent.velocity.magnitude);
        
        TextMeshProUGUI.transform.rotation = cinemachineFollow.transform.rotation;
    }
    
}
