using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int soGold = 0;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI.text = soGold.ToString();
    }
    private void Update()
    {
        textMeshProUGUI.text = soGold.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Coin"))     {
            AudioSource.Play();
            soGold++;
            Debug.Log("Nhat duoc 1 dong xu");
            Destroy(other.gameObject);
        }
    }
    public void SetCoin(int coin)
    {
        soGold += coin;
    }
}

