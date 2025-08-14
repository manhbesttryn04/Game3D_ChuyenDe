using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Fusion;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;


public class ChatNetWork : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("GameObject Chat")]
    public GameObject tatMoChat;
    [Header("New Chat")]
    public GameObject newChat;
    public TextMeshProUGUI newChatText;
    [Header("InputField Chat")]
    public TMP_InputField oNhanTin;
    [Header("Button Chat")]
    public Button nutMo;
    public Button nutTat;
    public Button nutGui;
    public Button nutGuiAdmin;
    [Header("Text Chat")]
    public TextMeshProUGUI tinNhan;
    [Header("Scroll Chat")]
    public ScrollRect cuonTinNhan;
    [Header("Name Player")]
    public CreateNameNetwork CreateNameNetwork;
    [Header("List Chat")]
    public List<string> allChatCurrent = new List<string>();
    public List<string> allChat = new List<string>();
    public int soTinNhanChuaDoc = 0;
    public void GetTenPlayer(Transform transform)
    {
        if (!HasInputAuthority || HasInputAuthority) ;
        //CreateNameNetwork = transform.gameObject.GetComponent<CreateNameNetwork>();
    }
    //Chat all player
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcGuiTinNhan(string noidung)
    {
        tinNhan.text += $"\n{noidung} ";
        Debug.Log("Da dc xuat len chat");

        oNhanTin.ActivateInputField();
        oNhanTin.text = "";
        allChatCurrent.Add(noidung);
        Canvas.ForceUpdateCanvases();
        cuonTinNhan.verticalNormalizedPosition = 0f;
    }

    //Chat admin
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcGuiTinNhanAdmin(string noidung)
    {
        tinNhan.text += $"\n{noidung} ";
        Debug.Log("Da dc xuat len chat");

        oNhanTin.ActivateInputField();
        oNhanTin.text = "";
        allChatCurrent.Add(noidung);
        Canvas.ForceUpdateCanvases();
        cuonTinNhan.verticalNormalizedPosition = 0f;
    }


    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;
        if (Input.GetKeyDown(KeyCode.Return) && oNhanTin.text != null)
        {

            Debug.Log("Gui tin nhan bang nut Enter");
        }

        if(nutMo.gameObject.activeSelf&& !nutTat.gameObject.activeSelf&& allChatCurrent.Count> allChat.Count)
        {
            soTinNhanChuaDoc = allChatCurrent.Count - allChat.Count;
            newChat.SetActive(true);
            newChatText.text = soTinNhanChuaDoc.ToString();

        }
        if (nutTat.gameObject.activeSelf && !nutMo.gameObject.activeSelf)
        {
            allChat = allChatCurrent.ToList();
            soTinNhanChuaDoc = 0;
            newChat.SetActive(false);
        }


    }
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Debug.Log("This is ADMIN");
        }else Debug.Log("This is PLAYER and not's ADMIN");
        nutMo.gameObject.SetActive(true);
        newChat.SetActive(false);
    }
    
    private void Start()
    {
        tatMoChat.SetActive(false);
        nutMo.gameObject.SetActive(false);
        nutTat.gameObject.SetActive(false);
        newChat.SetActive(false);
    }
    public void MoHopChat()
    {
        nutGuiAdmin.gameObject.SetActive(HasStateAuthority);
        tatMoChat.SetActive(true);
        nutMo.gameObject.SetActive(false);
        nutTat.gameObject.SetActive(true);

    }
    public void TatHopChat()
    {
        tatMoChat.SetActive(false);
        nutMo.gameObject.SetActive(true);
        nutTat.gameObject.SetActive(false);
        nutGuiAdmin.gameObject.SetActive(false);
    }
   

    public void GuiTinNhanDenSever()
    {
        if (HasInputAuthority || !HasInputAuthority)
        {
            if (string.IsNullOrWhiteSpace(oNhanTin.text)) return;
            string tingui;
            if (CreateNameNetwork == null)
            {
                tingui = $"<b>manh:</b> {oNhanTin.text}";
            }
            else
                tingui = $"<b>{CreateNameNetwork.GetNamePlayer()}:</b> {oNhanTin.text}";
            Debug.Log("Tin nhan da dc viet");

            RpcGuiTinNhan(tingui);
        }
    }
    public void GuiTinNhanAdminDenSever()
    {
        if(string.IsNullOrWhiteSpace(oNhanTin.text)) return;
        string tinguiadmin;
        tinguiadmin = $"<b><color=red>ADMIN</color>:</b> {oNhanTin.text}";

        RpcGuiTinNhanAdmin(tinguiadmin);

    }
    public void SetName(CreateNameNetwork createNameNetwork1)
    {
        CreateNameNetwork = createNameNetwork1;
    }


}








