using UnityEngine;

public class BagTaiNguyen : MonoBehaviour
{
    [SerializeField] GameObject Slots;
    public GameObject[] gameObjects = new GameObject[7];

    void Start()
    {
        SetUpSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpSlots()
    {
        for(int i = 0; i< gameObjects.Length; i++)
        {
            gameObjects[i] = gameObject;
            Instantiate(gameObject, gameObjects[i].transform);
        }
    }
}
