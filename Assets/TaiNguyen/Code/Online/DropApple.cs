using Fusion;
using UnityEngine;

public class DropApple : NetworkBehaviour
{
 
    public LayerMask appleTree;
    public Ray ray;
     public RaycastHit hit;

    private void Update()
    {
       
    }

    public override void FixedUpdateNetwork()
    {ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 10, appleTree))
        {
            Debug.Log("Ban dang dung gan cay tao");
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                var dropapple = hit.transform.gameObject.GetComponent<AppleTree>();
                if (dropapple != null)
                {
                    dropapple.SetTao();
                    Debug.Log("Ban da hai 1 qua tao");
                }

            }
        }
        else Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        
    }

}
