using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using Fusion;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera")]
    public CinemachineCamera camera;
    [SerializeField] public CinemachineThirdPersonFollow cam3rd;

    [Header("Nút chuột")]
    public int soLanBamChuot1 = 0;
    public int soLanbamChuot2 = 0;

    [Header("Góc nhìn")]
    public float x1 = 0f, y1, z1;
    public float x2 = 0.8f, y2 = 2.90f, z2 = -5.61f;

    [Header("Trái Táo")]
    public GameObject TraiTao;
    public GameObject TraiTao1;
    public bool anTao = false;
    public bool dangAnTao = false;
    public GameObject CanvasDangAnTao;
    [Header("Ánh xạ Network")]
    public PlayerAttackNetwork PlayerAttackNetwork;
    public DropApple DropApple;

    private void Start()
    {
        CanvasDangAnTao.SetActive(false);
        TraiTao.SetActive(false);
        TraiTao1.SetActive(false);
      
    }

    public void AssignCamera(Transform player)
    {
        camera.Follow = player;
        camera.LookAt = player;
        PlayerAttackNetwork = player.GetComponent<PlayerAttackNetwork>();
        DropApple = player.GetComponent<DropApple>();
    }

    private void Update()
    {
        GocNhinThu3();
        GocNhinChinhDien();
        GocNhinAnTao();
        BatDauAnTao();
        KiemTraTao();
       
    }
    

    // Góc nhìn thứ 3 (thay đổi khoảng cách camera)
    public void GocNhinThu3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && soLanBamChuot1 == 0)
        {
            Debug.Log("BAT1");
            cam3rd.ShoulderOffset = new Vector3(x2, y2, z2); // Góc xa
            soLanBamChuot1 = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && soLanBamChuot1 == 1)
        {
            Debug.Log("TAT1");
            cam3rd.ShoulderOffset = new Vector3(x1, y1, z1); // Góc gần
            soLanBamChuot1 = 0;
        }
    }

    // Góc nhìn chính diện (thu hẹp FOV)
    public void GocNhinChinhDien()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cam3rd.ShoulderOffset = new Vector3(x1, y1, z1); // Góc gần
            camera.Lens.FieldOfView = 10f;
            Debug.Log("BAT2");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            cam3rd.ShoulderOffset = (soLanBamChuot1 != 0)
                ? new Vector3(x2, y2, z2)  // Góc xa
                : new Vector3(x1, y1, z1); // Góc gần

            camera.Lens.FieldOfView = 60f;
            Debug.Log("TAT2");
        }
    }

    // Kích hoạt góc nhìn ăn táo và hiện trái táo
    public void GocNhinAnTao()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && soLanbamChuot2 == 0 )
        {
            cam3rd.ShoulderOffset = new Vector3(x1, y1, z1);    
            if(DropApple.GetAppleCount()<= 0){
                TraiTao.SetActive(false);
                Debug.Log("van con tao");
            }else if(DropApple.GetAppleCount() >=1){
                TraiTao.SetActive(true);
                Debug.Log("Khong co tao"); }

            
            PlayerAttackNetwork.SetAttack(false);
            soLanbamChuot2 = 1;
            Debug.Log("BAT3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && soLanbamChuot2 == 1 )
        {
            cam3rd.ShoulderOffset = new Vector3(x1, y1, z1);
            TraiTao.SetActive(false);
            PlayerAttackNetwork.SetAttack(true);
            soLanbamChuot2 = 0;
            Debug.Log("TAT3");
        }
    }

    // Khi click chuột trái trong trạng thái ăn táo
    public void BatDauAnTao()
    {
        if (Input.GetMouseButtonDown(0) && soLanbamChuot2 == 1 && DropApple.GetAppleCount() >= 1&& dangAnTao == false)
        {
            dangAnTao = true;
            anTao = true;
            if (anTao)
            {
                StartCoroutine(AnTao());
            }
            Debug.Log("Đã ăn táo");
        }
    }
  

    // Nếu ăn táo bị gián đoạn
    public void KiemTraTao()
    {
        if (soLanbamChuot2 == 1 && DropApple.GetAppleCount() >= 1 && anTao == false)
        {

            TraiTao.SetActive(true);
        }
        else if (soLanbamChuot2 == 1 && DropApple.GetAppleCount() <= 0)
        {
            TraiTao.SetActive(false);
        }
        else TraiTao.SetActive(false);
    }

    // Coroutine ăn táo
    public IEnumerator AnTao()
    {   
        CanvasDangAnTao.SetActive(true );
        TraiTao.SetActive(false );
        TraiTao1.SetActive(true);
   
        yield return new WaitForSeconds(3f);
        DropApple.SetAppleCount();
        CanvasDangAnTao.SetActive(false);
        TraiTao1.SetActive(false);
        anTao = false;
        dangAnTao = false;
        
    }
    public int GetSoPhim2()
    {
        return soLanbamChuot2;
    }
    public bool GetDangAnTao()
    {
        return dangAnTao;
    }
    
}
