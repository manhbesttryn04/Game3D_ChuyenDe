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
    public TickTimer tickTimer { get; set; }
    public bool isGame = false;

    public override void Spawned()
    {
        hienTenNhanVat.text = NameNetWork.ToString();
        hienTenNhatVatLocal.text = NameNetWork.ToString();

    }
    public override void FixedUpdateNetwork()

    {
        if (isGame)
        {
            isName = false;
            isName = true;
        }
        hienTenNhanVat.transform.LookAt(Camera.main.transform);
        hienTenNhanVat.transform.Rotate(0, 180, 0);

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

}




