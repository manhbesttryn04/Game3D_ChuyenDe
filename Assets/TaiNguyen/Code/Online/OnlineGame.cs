using System.Collections;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineGame : SimulationBehaviour, IPlayerJoined
{

    public GameObject[] PlayerSpaw;
    [Header("Chon nhan vat")]
    public GameObject chooserMode;
    public GameObject startGame;
    public int chooseChatacter;
    [Header("Tao ten nhan vat")]
    public GameObject createMode;
    public TMP_InputField nameCharacterBox;
    public TextMeshProUGUI canhBao;
    private string textCanhBaoBanDau = "Vui lòng nhập tên ít nhất 2 kí tự và nhiều nhất 10 kí tự!";
    public bool tatMoCanhBao = false;
    public string nameNetWork;
    public float vX;
    public float vY;
    public float vZ;


    private void Start()
    {
        chooserMode.SetActive(true);
        startGame.SetActive(false);
        canhBao.text = textCanhBaoBanDau;
    }
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("spaw");
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerSpaw[chooseChatacter], new Vector3(vX, vY, vZ), Quaternion.identity, player,
                (runner, obj) =>
                {
                    // Sau khi spawn xong, bạn có thể setup camera hoặc logic khác
                    var playerSetup = obj.GetComponent<PlayerSetUp>();
                    if (playerSetup != null)
                    {
                        playerSetup.SetUpCamera();
                    }
                    var playerCreateName = obj.GetComponent<CreateNameNetwork>();
                    if (playerCreateName != null)
                    {
                        playerCreateName.UpdateNameCharacter(nameNetWork);
                    }
                    var cam = obj.GetComponent<PlayerMove>();
                    if (cam != null)
                    {
                        Camera camera = Camera.main;

                        cam.setCamera(camera.transform);
                    }
                });


        }
    }
    public void ChooseHuman()
    {
        chooseChatacter = 0;
        Destroy(chooserMode);
        createMode.SetActive(true);
    }
    public void ChooseHulk()
    {
        chooseChatacter = 1;
        Destroy(chooserMode);
        createMode.SetActive(true);

    }
    public void CreateName()
    {
        if (nameCharacterBox.text.Length < 2 || nameCharacterBox.text.Length == 0)
        {
            canhBao.text = "Tên ít hơn 2 kí tự, vui lòng nhập lại!";
            tatMoCanhBao = true;
            if(tatMoCanhBao)
            {
                StartCoroutine(TatMoCanhBao());
            }

        }
        else if (nameCharacterBox.text.Length > 10)
        {
            canhBao.text = "Tên quá 10 kí tự, vui lòng nhập lại!";
            tatMoCanhBao = true;
            if (tatMoCanhBao)
            {
                StartCoroutine(TatMoCanhBao());
            }
        }
        else
        {
            nameNetWork = nameCharacterBox.text;
            createMode.SetActive(false);
            startGame.SetActive(true);
        }


    }
    public IEnumerator TatMoCanhBao()
    {
        yield return new WaitForSeconds(1.5f);
        canhBao.text = textCanhBaoBanDau;
        tatMoCanhBao = false;
    }

}
