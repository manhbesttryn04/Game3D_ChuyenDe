using Fusion;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerSetUp : NetworkBehaviour
{
    public int soPhimTao;
    public bool dangAnTao;
    public CameraFollow camera;
    public ChatNetWork chatNetWork;
   public void SetUpCamera()
    {
        if (Object.HasInputAuthority)
        {
             camera = FindObjectOfType<CameraFollow>();
            if (camera != null){
                camera.AssignCamera(transform);
                soPhimTao = camera.GetSoPhim2();
                dangAnTao = camera.GetDangAnTao();

            }
           
          
        }

    }
    public void GetTenPlayer()
    {if(Object.HasInputAuthority) {
        ChatNetWork chat = FindAnyObjectByType<ChatNetWork>();
        if (chat != null)
        {
            chat.GetTenPlayer(transform);
        }
    } }
    
    public override void Spawned()
    {
       
        Debug.Log(transform.GetComponent<CreateNameNetwork>().GetNamePlayer().ToString());
    }
   
    
    public override void FixedUpdateNetwork()
    {if (!Object.HasInputAuthority) return; 
        soPhimTao = camera.GetSoPhim2();
        dangAnTao = camera.GetDangAnTao();
    }
    public int GetSoPhim()
    {
        return soPhimTao;
    }
    public bool GetDangAnTao()
    {
        return dangAnTao;
    }
}
