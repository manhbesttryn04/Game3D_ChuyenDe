using System.Collections;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
  
{
    [SerializeField] NavMeshAgent agent;
     public Transform viTriBanDauCuaNPC;
    [SerializeField] Transform viTriCuaPlayer;

    [SerializeField] GameObject hoiThoaiGiuaPlayerVaNPC;
    [SerializeField] GameObject batDauNhiemVu;
    [SerializeField] GameObject hienCanhBao;
    [SerializeField] GameObject hienGiet1ConNaiGia;

    [SerializeField] TextMeshProUGUI textCanhBao;
    [SerializeField] TextMeshProUGUI textGietNai;
    [SerializeField] TextMeshProUGUI textSoConNai;
    [SerializeField] TextMeshProUGUI textKetQuaNhiemVu;

    public float khoangCachNPCChayTheo;
    public float khoangCachDeBatDauHoiThoai;
    public int soConNaiFake = 3;
    public int soConNaiReal = 7;
    public float thoiGianNhiemVu = 5f;
    public float tocDoThoiGian = 0.01f;

    public bool canhBao = true;
    public bool tinVui = true;
    public bool canhBaoTiepTheo = true;
    public bool tinVuiDaGiet = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoiThoaiGiuaPlayerVaNPC.SetActive(false);
        batDauNhiemVu.SetActive(false);
        hienCanhBao.SetActive(false);

        viTriBanDauCuaNPC.position = transform.position;
        textSoConNai.text = $"{soConNaiFake}/3";
        textKetQuaNhiemVu.text = "Nhiệm vụ: Chưa hoàn thành";

    }

    // Update is called once per frame
    void Update()
       
        
    {
       //Tinh thoi gian
       thoiGianNhiemVu -= tocDoThoiGian * Time.deltaTime;
        //Tinh khoang cach nguoi choi va npc
        textSoConNai.text = $"{soConNaiFake}/3";
        var _khoangCachNPCVaPlayer = Vector3.Distance(viTriBanDauCuaNPC.position, viTriCuaPlayer.position);
        if(_khoangCachNPCVaPlayer <= khoangCachNPCChayTheo)
        {
            agent.Move( new Vector3(viTriCuaPlayer.position.x - 1, viTriCuaPlayer.position.y, viTriCuaPlayer.position.z - 1));
            transform.rotation = agent.transform.rotation;

            var _khoangCachHoiThoai = Vector3.Distance(viTriBanDauCuaNPC.position, viTriCuaPlayer.position);
            if(_khoangCachHoiThoai <= khoangCachDeBatDauHoiThoai)
            {
                hoiThoaiGiuaPlayerVaNPC.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Return)) {
                    hoiThoaiGiuaPlayerVaNPC.SetActive(false);
                    batDauNhiemVu.SetActive(true);
                    agent.Move(viTriBanDauCuaNPC.position);
                }
            }
            //Canh bao
            if (soConNaiReal > 1 && soConNaiReal < 7 && canhBao == true && canhBaoTiepTheo == true)
            {

                StartCoroutine(HienCanhBao());
                canhBaoTiepTheo = false;

            }
            //Canh bao sap thua
            else if (soConNaiReal == 1 && canhBao == true && canhBaoTiepTheo == true)
            {
                StartCoroutine(HienCanhBaoSapThua());
                canhBaoTiepTheo= false;
                
            }
            //Tin vui
            if( soConNaiFake < 3 && tinVui == true&& tinVuiDaGiet == true)
            {
                StartCoroutine(HienGietNai(soConNaiFake));
                tinVuiDaGiet = false;
            }
            //Ket qua
            if(soConNaiFake == 0 && soConNaiReal >=1&& thoiGianNhiemVu > 0) {
                textKetQuaNhiemVu.text = "Nhiệm vu: Hoàn Thành";
            }
            else if(soConNaiReal == 0 && soConNaiFake >=1)
            {
                textKetQuaNhiemVu.text = "Nhiệm vụ: Chưa hoàn thành và đã thất bại";
            }
            //Thua vi het gio
           else if (thoiGianNhiemVu <= 0f && soConNaiFake >= 1)
            {
                textKetQuaNhiemVu.text = "Nhiệm vụ: Chưa hoàn thành và đã thất bại";
            }
            
        }
    }
    IEnumerator HienCanhBao()
    {
        canhBao = false ;
            var _ranDomCanhBao = Random.Range(1, 3);
            if (_ranDomCanhBao == 1)
            {
                textCanhBao.text = "Bạn đã sai!";
                hienCanhBao.SetActive(true);
                yield return new WaitForSeconds(3);
                hienCanhBao.SetActive(false);
            canhBao = true ;
                yield break;
            }
            else if (_ranDomCanhBao == 2)
            {
                textCanhBao.text = $"Bạn đã giết sai rồi, hiện chỉ còn {soConNaiReal} con nai.";
                hienCanhBao.SetActive(true);
                yield return new WaitForSeconds(3);
                hienCanhBao.SetActive(false);
            canhBao = true;
            yield break;
            }
        
        
    }
    IEnumerator HienCanhBaoSapThua()
    { canhBao = false;
        textCanhBao.text = "Bạn đã giết 6 con thiệt rồi, còn 1 cơ hội cuối.";
        hienCanhBao.SetActive(true);
        yield return new WaitForSeconds(3);
        hienCanhBao .SetActive(false);
        canhBao = true;
    }
    IEnumerator HienGietNai(int i)
    {
        tinVui = false;
        if(i == 2)
        {
            textGietNai.text = "Quá êm, bạn đã giết 1 con giả";
            hienGiet1ConNaiGia.SetActive(true);
            yield return new WaitForSeconds(3);
            hienGiet1ConNaiGia .SetActive(false);
            tinVui = true;
            yield break ;
        }
        else if(i == 1)
        {
            textGietNai.text = "LỤMMM, 2 con rồi!, còn 1 con nữa.";
            hienGiet1ConNaiGia.SetActive (true);
            yield return new WaitForSeconds(3);
            hienGiet1ConNaiGia.SetActive(false);
            tinVui = true;
            yield break ;
        }else if(i == 0)
        {
            textGietNai.text = "Bạn đã giết đúng con nai rồi, xin chúc mừng!";
            hienGiet1ConNaiGia.SetActive(true);
            yield return new WaitForSeconds(3);
            hienGiet1ConNaiGia.SetActive(false) ;
            tinVui = true;
            yield break ;
        }
    }
    public int SoNaiRealHienTai()
    {
        return soConNaiReal;
    }
    public int SoNaiFakeHienTai()
    {
        return soConNaiFake;
    }
    public void GietNaiReal()
    {
        soConNaiReal -= 1;
    }
    public void GietNaiFake()
    {
        soConNaiFake -= 1;
    }

    

    
}
