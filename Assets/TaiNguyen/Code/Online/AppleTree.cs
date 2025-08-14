using System.Collections;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

public class AppleTree : NetworkBehaviour
{

    [Networked]
    public int AppleCount { get; set; }

    public GameObject[] apples;
    public GameObject appleTextPopup;
    public TextMeshProUGUI appleTextUI;

    private int lastAppleCount;
    private float applesTaken;

    public override void Spawned()
    {
        AppleCount = apples.Count(apple => apple.activeSelf);
        lastAppleCount = AppleCount;
        appleTextPopup.SetActive(false);
    }

    public override void FixedUpdateNetwork()
    {
        lastAppleCount = AppleCount;
    }

    // RPC để gửi từ client tới host yêu cầu nhặt táo
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcTruTao()
    {
       RpcXuLyTruTao();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcXuLyTruTao()
    {
        HandleTruTao();
    }
    public void HandleTruTao()
    {
        GameObject firstActiveApple = apples.FirstOrDefault(apple => apple.activeSelf);
        if (firstActiveApple != null)
        {
            firstActiveApple.SetActive(false);
        }
        if (HasStateAuthority)
        {
            AppleCount--;
        }
     
        applesTaken = 1f;
        appleTextUI.text = $"+{lastAppleCount - AppleCount} Apple";
        lastAppleCount = AppleCount;
        appleTextPopup.SetActive(true);
        StartCoroutine(Riset());
    }
    public IEnumerator Riset()
    {
        yield return new WaitForSeconds(0.3f);
        appleTextPopup.SetActive(false);
    }

    

    // Gọi từ client
    public void RequestTruTao()
    {
        if (HasInputAuthority)
        {
            RpcTruTao();
        }
    }

    public int GetAppleCount() => AppleCount;

    public float GetApplesTaken() => applesTaken;
}
