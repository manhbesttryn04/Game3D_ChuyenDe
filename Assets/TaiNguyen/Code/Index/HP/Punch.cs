using Unity.VisualScripting;
using UnityEngine;

public class Punch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            Debug.Log("cham vs human");
            other.gameObject.GetComponent<NetworkHealth>().TruHealth(5);
        }
    }
}
