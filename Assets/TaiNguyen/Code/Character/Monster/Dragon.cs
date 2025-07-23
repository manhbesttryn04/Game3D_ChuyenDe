
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dragon  : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform mucTieu;
    private Vector3 _vitriBandau;
    [SerializeField] private float phamviTancong;
    [SerializeField] private Animator animator;
    private bool thaydoidiemden;
    [SerializeField] private HP Maurong;
    [SerializeField ] DropItem DropItem; 
    public bool nhatVatPham = true;
    [SerializeField] public GameObject DAME;
    [SerializeField] public TextMeshProUGUI  dameText;
   
    //[SerializeField] NV NV;
    [SerializeField] AudioSource dragonSource;
    [SerializeField] AudioClip dragonClip;
    [SerializeField] GameObject mau1;
    [SerializeField] GameObject mau2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Quái vật có phạm vi tấn công
    //Nếu nhân vật vào phạm vi này thì quái sẽ đuổi theo
    //Nếu nhân vật ra khỏi phạm vi thì quái dừng đuổi
    // quay về vị trí ban đầu
    //Quái tự động di chuyển xung quanh
    void Start()
    {    DAME.SetActive(false);
        
        _vitriBandau = transform.position;
       
        dameText.text = "0";
    }
   
    
    // Update is called once per frame
    void Update()
    {
        
        //Tính khoảng cách giữa nhân vật và quái
        var distance = Vector3.Distance(mucTieu.position, _vitriBandau);
        //Xet khoang cach rong tan cong player
        if (distance <= phamviTancong)
        {
            dragonSource.PlayOneShot(dragonClip);
            agent.SetDestination(mucTieu.position );
            distance = Vector3.Distance(mucTieu.position, transform.position);
            if (distance <= 2)
            {
                animator.SetTrigger("Tan cong");
            }
            else if (distance <= 2 && Maurong.GetHPHienTai() <= 0)
            {
                animator.SetBool("Die", true);
            }
           
        }
        else if(distance > phamviTancong && Maurong.GetHPHienTai() >0)
        {
            //quay về vị trí ban đầu
           
            agent.SetDestination(_vitriBandau);
            distance = Vector3.Distance(_vitriBandau, transform.position);
            if(distance <= 5) {
                if(!thaydoidiemden)
                {
                    thaydoidiemden = true;
                    //SAU 5 GIAY
                    Invoke(nameof(ChangeDestination), 5);
                }
               
                
            }
        }
      
        if(Maurong.GetHPHienTai() <= 0&& nhatVatPham == true)
        {
            DropItem.DropRandomItems(transform.position)
                ;
           animator.SetBool("Die",true);
            Destroy(gameObject,2f);
            nhatVatPham = false;
            animator.SetFloat("Di chuyen", 0);
           // NV.SetNV();
        }
        animator.SetFloat("Di chuyen", agent.velocity.magnitude);
    }
        private void ChangeDestination()
    {
        thaydoidiemden = false;
        var ramdomvitri = Random.insideUnitSphere * 5;
        ramdomvitri += _vitriBandau;
        NavMeshHit hit;
        NavMesh.SamplePosition(ramdomvitri, out hit, 5, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Debug.Log(".....Matmau");
            int dame = Random.Range(1, 40);
            Debug.Log(dame);
            Maurong.TakeDamage(dame);
            DAME.SetActive(true);
            dameText.text = dame.ToString();
            
            if(dame > 10)
            {
                dameText.color = Color.red;
            }
         var ramdom = Random.Range(1,2);
            if (ramdom == 1)
            {
                GameObject maume = Instantiate(mau1, transform.position, Quaternion.identity);
               
            }
            else if(ramdom == 2) { GameObject maume2 = Instantiate(mau2, transform.position, Quaternion.identity); }
            Debug.Log(ramdom);
            StartCoroutine(Xoachu());
           
            
        
            
            
        }
    }
    IEnumerator Xoachu()
    {
        yield return new WaitForSeconds(0.1f);
        dameText.text = "0";
        DAME.SetActive(false);
        dameText.color = Color.white;
    }
}
