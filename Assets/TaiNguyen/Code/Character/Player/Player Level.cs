using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] TextMeshProUGUI textLevelMax;
    [SerializeField] Slider slider;
    public float exp;
    public float expBanDau = 0;
    [SerializeField] TextMeshProUGUI levels;

    void Start()
    {
        levels.text = playerInfo.GetLevel().ToString();
        slider.maxValue = playerInfo.GetSoLevel();
        slider.value = playerInfo.GetExp();
        textLevelMax.text = playerInfo.GetSoLevel().ToString();
        textLevel.text = playerInfo.GetExp().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        levels.text = playerInfo.GetLevel().ToString();
        slider.maxValue = playerInfo.GetSoLevel();
        slider.value = playerInfo.GetExp();
        
        textLevelMax.text =  "/"+ " "+playerInfo.GetSoLevel().ToString();
        textLevel.text = playerInfo.GetExp().ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EXP")) {
            playerInfo.AddExp(exp);
            expBanDau += exp;
            Destroy(other.gameObject);
            Debug.Log("Nhat exp");
        }
    }
}
