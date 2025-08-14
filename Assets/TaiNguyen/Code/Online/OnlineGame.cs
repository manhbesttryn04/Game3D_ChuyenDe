using System.Collections;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineGame : SimulationBehaviour, IPlayerJoined
{
    [Header("Player Spawn")]
    public GameObject[] PlayerSpaw;
    [Header("Image Chatacter")]
    public Sprite[] imageChatacter;
    [Header("Choose Character")]
    public GameObject chooserMode;
    public int chooseChatacter;
    public GameObject createMode;
    public TMP_InputField nameCharacterBox;
    public TextMeshProUGUI canhBao;
    private string textCanhBaoBanDau = "Vui lòng nhập tên ít nhất 2 kí tự và nhiều nhất 10 kí tự!";
    public bool tatMoCanhBao = false;
    [Header("Image Character")]
    public Image imageChatacterBox;
    [Header("Start Game")]
    public GameObject startGame;
    [Header("Name Network")]
    public string nameNetWork;
   
    public int indexChatacter;
    [Header("Transform Spaw")]
    public float vX;
    public float vY;
    public float vZ;
   

    private void Start()
    {
        //chooserMode.SetActive(true);
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
                    var nameplayer = obj.GetComponent<PlayerSetUp>();
                    if (nameplayer != null)
                    {
                        nameplayer.GetTenPlayer();
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
                    var imageChatacterPlayer = obj.GetComponent<ImagePlayerLocal>();
                    if(imageChatacterPlayer != null)
                    {
                        imageChatacterPlayer.SetImage(imageChatacter[indexChatacter]);
                    }
                });
            chooserMode.SetActive(false);


        }
       
      
    }
    public void ChooseHuman1()
    {
        chooseChatacter = 0;
        indexChatacter = 0;
        chooserMode.SetActive(false);
        createMode.SetActive(true);
    }
    public void ChooseHuman2()
    {
        chooseChatacter = 1;
        indexChatacter = 1;
        chooserMode.SetActive(false);
        createMode.SetActive(true);

    }
    public void CreateName()
    {
        if (nameCharacterBox.text.Length < 2 || nameCharacterBox.text.Length == 0)
        {
            canhBao.text = "Tên ít hơn 2 kí tự, vui lòng nhập lại!";
            tatMoCanhBao = true;
            if (tatMoCanhBao)
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
