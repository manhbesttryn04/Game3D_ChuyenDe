using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackGun : MonoBehaviour
{
    [SerializeField] public Gun Gun;
    [SerializeField] Transform leftGun;
    [SerializeField] Transform rightGun;

    [SerializeField] GameObject vienDan;
    [SerializeField] Transform tamBan;
    public int tocDoBan = 500;
    public int luotBanSungLuc = 0;
    public int soLanBan = 0;
    public int soLanBan1 = 0;
    public float thoiGianNapDanSungLuc = 3;
    [SerializeField] public GameObject nuNapDan;
    [SerializeField] public TextMeshProUGUI TextThayDan;



    void Start()
    {
        nuNapDan.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BanSungTruong();
        BanSungLuc();

    }
    public void BanSungLuc()
    {
        if (Gun.GetSungLuc() == true)
        {


            if (Input.GetButtonDown("Fire1") && luotBanSungLuc == 0)
            {
                GameObject ban = Instantiate(vienDan, leftGun.position, Quaternion.identity);
                Rigidbody rb = ban.GetComponent<Rigidbody>();

                rb.linearVelocity = tamBan.forward * tocDoBan;
                Gun.SetDanSungLuc(1);
                Destroy(rb, 2f);
                soLanBan++;
                luotBanSungLuc = 1;

            }
            else if (Input.GetButtonDown("Fire1") && luotBanSungLuc == 1)
            {
                GameObject ban = Instantiate(vienDan, rightGun.position, Quaternion.identity);
                Rigidbody rb = ban.GetComponent<Rigidbody>();

                rb.linearVelocity = tamBan.forward * tocDoBan;
                Gun.SetDanSungLuc(1);
                Destroy(rb, 2f);
                soLanBan++;
                luotBanSungLuc = 0;
            }
        }
        if (Gun.GetDanSungLuc() <= 0)
        {

            soLanBan = 0;
            Gun.SetSungLuc(false);
        }

        if (soLanBan >= 30 && Gun.GetDanSungLuc() >= 1)
        {
            nuNapDan.SetActive(true);
            Gun.SetSungLuc(false);
            TextThayDan.text = "Nạp đạn(E)";


        }
        if (Input.GetKeyDown(KeyCode.E)&& soLanBan >=30)
        {
            nuNapDan.SetActive(false);
            TextThayDan.text = "Đang nạp đạn...";
            StartCoroutine(NapDanSungLuc(3));

        }
    
    } 
    public void BanSungTruong()
    {
        if (Gun.GetSungLong() == true)
        {


            if (Input.GetButton("Fire1"))
            {
                GameObject ban = Instantiate(vienDan, leftGun.position, Quaternion.identity);
                Rigidbody rb = ban.GetComponent<Rigidbody>();

                rb.linearVelocity = tamBan.forward * tocDoBan;
                Destroy(ban,2f);
                Gun.SetSoDanSungTruong(1);
                soLanBan1++;


            }
            //Het Dan sung truong
           


        }
        if (Gun.GetDanSungTruong() <= 0)
        {


            soLanBan1 = 0;
            Gun.SetSungTruong(false);
        }
        //Nap dan sung Truong
        if (soLanBan1 >= 40 && Gun.GetDanSungTruong() >= 1)
        {
            nuNapDan.SetActive(true);
            Gun.SetSungTruong(false);
            TextThayDan.text = "Nạp đạn(E)";


        }
        if (Input.GetKeyDown(KeyCode.E) && soLanBan1 >= 40)
        {
            nuNapDan.SetActive(false);
            TextThayDan.text = "Đang nạp đạn...";
            StartCoroutine(NapDanSungTruong(3));


        }
    }
    IEnumerator NapDanSungLuc(float time)
    {
        Debug.Log("Dang nap dan i ");
        yield return new WaitForSeconds(time);
        soLanBan = 0;
        Gun.SetSungLuc(true);
        nuNapDan.SetActive(false);
    }
    IEnumerator NapDanSungTruong(float time)
    {
        Debug.Log("Dang nap dan x");
        yield return new WaitForSeconds(time);
        soLanBan1 = 0;
        Gun.SetSungTruong(true);
        nuNapDan.SetActive(false);
    }

}

