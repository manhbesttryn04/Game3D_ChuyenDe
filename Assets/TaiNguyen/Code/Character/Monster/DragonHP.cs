using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class DragonHP : MonoBehaviour
{
    [SerializeField] private HP hP ;
    [SerializeField] private Slider Slider;
    [SerializeField] private CinemachineFollow CinemachineFollow;
    public bool dragonHealth = false;
    [SerializeField] Heathling Heathling;
    public Vector3 viTriBanDau;

    private void Start()
    {
        viTriBanDau = transform.position;
        Slider.maxValue = hP.GetHpMax();
    }
    private void Update()
    {
        if(hP.GetHPHienTai() < hP.GetHpMax())
        {if(transform.position.x == viTriBanDau.x)
            dragonHealth = true;
            Heathling.BatDauHoiMau(dragonHealth);
        }
        Slider.value = hP.GetHPHienTai();
        Slider.transform.rotation = CinemachineFollow.transform.rotation;   
    }
    public float GetHPDragon() {
        return hP.GetHPHienTai();
    }

}
