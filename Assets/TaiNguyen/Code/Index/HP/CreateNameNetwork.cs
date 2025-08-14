using System.Collections;
using Fusion;
using TMPro;
using UnityEngine;

public class CreateNameNetwork : NetworkBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Networked, OnChangedRender(nameof(CreateNameCharacter))]
    public bool isName { get; set; } = true;
    [Networked]
    public NetworkString<_16> NameNetWork { get; set; }
    public TextMeshProUGUI hienTenNhanVat;
    public TextMeshProUGUI hienTenNhatVatLocal;
    public ChatNetWork chatNetWork;
    public bool isGame = false;

    public override void Spawned()
    {
        hienTenNhanVat.text = NameNetWork.ToString();
        hienTenNhatVatLocal.text = NameNetWork.ToString();
        chatNetWork = FindAnyObjectByType<ChatNetWork>();

    }
    public override void FixedUpdateNetwork()

    {
        if (!Object.HasInputAuthority) return;
        if (isGame)
        {
            isName = false;
            isName = true;
        }
        
        chatNetWork.SetName(gameObject.GetComponent<CreateNameNetwork>());
        
    }
    public void CreateNameCharacter()
    {
        if (isName)
        {
            hienTenNhanVat.text = NameNetWork.ToString();
            hienTenNhatVatLocal.text = NameNetWork.ToString();
            Debug.Log("Da tao ten");
        }
    }

    public void UpdateNameCharacter(string name)
    {
        NameNetWork = name;
        isGame = true;
        Debug.Log("da them ten");

    }
    public NetworkString<_16> GetNamePlayer()
    {
        return NameNetWork;
    }

}




