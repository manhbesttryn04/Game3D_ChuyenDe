using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHealth : NetworkBehaviour
{
    //Khai bao
    [Networked, OnChangedRender(nameof(ChangleHealthNetWork))]
    public float Health { get; set; }
    public GameObject healthNetwork;
    [SerializeField] Slider healthSlider;
    public GameObject healthLocal;
    [SerializeField] Slider healthSiderLocal;
    public GameObject textADMINLocal;

    private void Start()
    {
        
        healthSlider.value = Health;
        healthSiderLocal.value = Health;
        if (Object.HasInputAuthority)
        {
            textADMINLocal.SetActive(Runner.IsServer);
            healthLocal.SetActive(true);
            healthNetwork.SetActive(false);
        }
        else
        { textADMINLocal.SetActive(false);
            healthLocal.SetActive(false);
            healthNetwork.SetActive(true);
        }

    }
    public void ChangleHealthNetWork()
    {
        if (Object.HasInputAuthority)
        {
            healthLocal.SetActive(true);
            healthNetwork.SetActive(false);
        }
        else
        {
            healthLocal.SetActive(false);
            healthNetwork.SetActive(true);
        }
        healthSlider.value = Health;
        healthSiderLocal.value = Health;

    }

    public void TruHealth(int hp)
    {
        Health -= hp;
    }
    public float GetHp()
    {
        return Health;
    }





}
