using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool GunShort = false;
    public bool GunLong = false;
    public RaycastHit hit;
    public Ray Ray;
    public LayerMask sungLuc;
    public LayerMask sungTruong;
    [SerializeField] public GameObject textLumSung;
    public int soDanSungLuc = 0;
    public int soDanSungTruong = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(Ray,out hit, 20f, sungLuc)){
            Debug.Log("Phat hien cay sung luc");
            textLumSung.SetActive(true);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            if (Input.GetKeyDown(KeyCode.F))
            {
                soDanSungLuc = 90;
                soDanSungTruong = 0;
                GunShort = true;
                GunLong = false;
                Destroy(hit.transform.gameObject);

            }
        }
        else if(Physics.Raycast(Ray,out hit, 20f, sungTruong))
        {
            textLumSung.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

                if (Input.GetKeyDown(KeyCode.F))
                    soDanSungTruong = 120;
                soDanSungLuc = 0;
                    GunLong = true;
                GunShort = false;
                Debug.Log("so cua" + GunLong);
                Destroy(hit.transform.gameObject);
            }

        }else

        {
            textLumSung.SetActive(false);
           
            
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        }


    }
    public bool GetSungLuc()
    {
        return GunShort;
    }
    public bool GetSungLong()
    {
        return GunLong;
    }
    public void SetSungLuc(bool i)
    {
        GunShort = i;
    }
    public void SetSungTruong(bool i)
    {
        GunLong = i;
    }
    public int GetDanSungLuc()
    {
        return soDanSungLuc;
    }
    public int GetDanSungTruong()
    {
        return soDanSungTruong;
    }
    public void SetDanSungLuc(int i)
    {
        soDanSungLuc -= i;
    }
    public void SetSoDanSungTruong(int i) {
        soDanSungTruong -= i;
    }
}
