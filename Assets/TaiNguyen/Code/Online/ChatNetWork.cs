using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatNetWork : NetworkBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject tatMoChat;
    public TMP_InputField oNhanTin;
    public Transform choGuiLen;
    public GameObject tinNhan;
    public GameObject nutMo;
    public GameObject nutTat;
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_GuiTinNhan(string noidung)
    {
        Debug.Log("Da up len ");
        NetworkObject tin = Runner.Spawn(
    tinNhan.GetComponent<NetworkObject>(),
    choGuiLen.position,
    Quaternion.identity,
    null);

        tin.GetComponent<TextMeshProUGUI>().text = noidung;


    }
    private void Start()
    {
        tatMoChat.SetActive(false);
        nutMo.SetActive(true);
        nutTat.SetActive(false);
    }
    public void MoHopChat()
    {  tatMoChat.SetActive(true);
           nutMo.SetActive(false);
        nutTat.SetActive(true);
        }
    public void TatHopChat()
    {
        tatMoChat.SetActive(false);
        nutMo.SetActive(true);
        nutTat.SetActive(false);
    }
    
    



    public void GuiTinNhan()
    { if (string.IsNullOrWhiteSpace(oNhanTin.text)) return;
     string tingui = oNhanTin.text;
        RPC_GuiTinNhan(tingui);
       



        oNhanTin.text = "";
        oNhanTin.ActivateInputField();



    }
    



}
