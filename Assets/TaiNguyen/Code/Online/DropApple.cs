using System.Collections;
using Fusion;
using TMPro;
using UnityEngine;

public class DropApple : NetworkBehaviour
{
 
    public LayerMask appleTree;
    public Ray ray;
     public RaycastHit hit;
    public GameObject nutNhat;
    [Networked,OnChangedRender(nameof(HienSoTaoDangCo))]
    public int soTao { get; set; } = 0;
    public TextMeshProUGUI hienSoTao;

    public void HienSoTaoDangCo()
    {
        hienSoTao.text = soTao.ToString();  

    }
    private void Start()
    {
            hienSoTao.text = soTao.ToString();
            nutNhat.SetActive(false);
        
    }
       
    

    public override void FixedUpdateNetwork()
       
    {if (Object.HasInputAuthority)
        {
            ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, 10, appleTree))
            {
                Debug.Log("Ban dang dung gan cay tao");
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
                nutNhat.SetActive(true);

                if (hit.transform.GetComponent<AppleTree>().GetAppleCount() > 0 && Input.GetKeyDown(KeyCode.E))
                {
                    var dropapple = hit.transform.GetComponent<AppleTree>();
                    if (dropapple != null)
                    {
                        dropapple.RpcTruTao();
                        soTao += (int)dropapple.GetApplesTaken();
                        Debug.Log("Ban da hai 1 qua tao");

                    }

                }
                else if (hit.transform.GetComponent<AppleTree>().GetAppleCount() <= 0 && Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Het tao de hai   ");

                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
                nutNhat.SetActive(false);
            }
        }
   
    }
    public int GetAppleCount()
    {
        return soTao;
    }
    public void SetAppleCount()
    {
        soTao -= 1;
    }
    public void GiamTaoDangCo()
    {
        soTao--;
    }
    
   


}
