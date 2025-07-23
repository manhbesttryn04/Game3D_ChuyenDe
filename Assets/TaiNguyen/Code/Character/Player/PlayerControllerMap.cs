using UnityEngine;

public class PlayerControllerMap : MonoBehaviour
{
    [SerializeField] MapGenerator map;
    [SerializeField] MapGenerator2 map2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("SnowZone"))
        {
            map.SetSnowZone(true);
        }else map.SetSnowZone(false);
       if(hit.gameObject.CompareTag("Center Zone"))
        {
            map2.SetZoneCenter(true);
            Debug.Log("cham vs map");
            Destroy(hit.gameObject);
        }else if(hit.gameObject.CompareTag("Right Zone"))
        {
            map2.SetZoneRight(true);
            Destroy(hit.gameObject);
        }else if(hit.gameObject.CompareTag("Leflt Zone"))
        {
            map2.SetZoneLeflt(true);
            Destroy(hit.gameObject);
        }
    }
}
