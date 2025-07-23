using Fusion;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerSetUp : NetworkBehaviour
{
   
   public void SetUpCamera()
    {
        if (Object.HasInputAuthority)
        {
            CameraFollow camera = FindObjectOfType<CameraFollow>();
            if (camera != null){
                camera.AssignCamera(transform);
            }
           
          
        }

    }
}
