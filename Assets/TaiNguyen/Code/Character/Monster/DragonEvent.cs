using UnityEngine;

public class DragonEvent : MonoBehaviour
{
    [SerializeField] private BoxCollider BoxCollider;
   public void Drbatdautancong()
    {
        BoxCollider.enabled = true;
    }
    public void Drketthuctancong()
    {
        BoxCollider.enabled = false;    
    }
    
}
