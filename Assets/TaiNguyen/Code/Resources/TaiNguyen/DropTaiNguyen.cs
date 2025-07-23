using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DropTaiNguyen : MonoBehaviour
{
    [SerializeField] public LayerMask TreeLayer;
    [SerializeField] public LayerMask RockLayer;
    [SerializeField] public TaiNguyen TaiNguyen;
    [SerializeField] TextMeshProUGUI thongTinVatPham;
    [SerializeField] GameObject nutNhat;
    [SerializeField] TextMeshProUGUI textWood;
    [SerializeField] TextMeshProUGUI textRock;
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textLa;
    [SerializeField] public AudioSource nhatDo;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip nhatrock;
    public int countChat = 0;

    void Start()
    {
        nutNhat.SetActive(false);
        textWood.text = "0";
        textRock.text = "0";
        textLa.text = "0";
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Ưu tiên kiểm tra cây trước
        if (Physics.Raycast(ray, out hit, 4, TreeLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            nutNhat.SetActive(true);
            thongTinVatPham.text = "Nhặt Cây (E)";
          textName.text = hit.transform.name;
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Chat");
                countChat++;
                if(countChat == 2) {
                    DropWood(hit.transform.gameObject);
                    Debug.Log("Đã nhặt cây va la");
                    textWood.text = TaiNguyen.GetTree().ToString();
                    textLa.text = TaiNguyen.GetLeaf().ToString();
                    countChat = 0;
                }
               
                
                
            }
        }
        // Nếu không phải cây, kiểm tra đá
        else if (Physics.Raycast(ray, out hit, 2, RockLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.cyan);
            nutNhat.SetActive(true);
            thongTinVatPham.text = "Nhặt Đá (E)";
            textName.text = hit.transform.name;
            if (Input.GetKeyDown(KeyCode.E))
            {
                DropRock(hit.transform.gameObject);
                Debug.Log("Đã nhặt 1 cục đá");
                textRock.text = TaiNguyen.GetRock().ToString();
                nhatDo.PlayOneShot(nhatrock);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 2, Color.yellow);
            nutNhat.SetActive(false);
            
        }
    }

    public void DropWood(GameObject gameObject)
    {
        
        TaiNguyen.AddTree(2);
        TaiNguyen.AddLeaf(20);
       
        StartCoroutine(ngacay(gameObject));
        
      }

    public void DropRock(GameObject gameObject)
    {
        TaiNguyen.AddRock(1);
        Destroy(gameObject);
        animator.SetTrigger("Nhat");
        
       
    }
    public IEnumerator ngacay(GameObject gameObject)
    {
        nutNhat.SetActive(false);
        float tocdongacay = 0f;
        Quaternion StartNgacay = gameObject.transform.rotation;
        Quaternion endNgacay = Quaternion.Euler(90f, gameObject.transform.eulerAngles.y, 0f);
     
        yield return new WaitForSeconds(0.1f);
        
        while(tocdongacay < 1f)
        {
            gameObject.transform.rotation = Quaternion.Lerp(StartNgacay,endNgacay,tocdongacay);
            tocdongacay += 0.05f;
            yield return null;

                   }
        gameObject.transform.rotation = endNgacay;
                  Destroy(gameObject, 3f);
        
    }
}
