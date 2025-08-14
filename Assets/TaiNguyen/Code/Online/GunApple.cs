using Fusion;
using UnityEngine;

public class GunApple : NetworkBehaviour
{
   
    public PlayerSetUp PlayerSetUp;
    public DropApple DropApple;
    public Transform viTriBanTao;
    public GameObject TraiTaoRef;
    
    public override void FixedUpdateNetwork()
    {if (!Object.HasInputAuthority) return;
        if(Input.GetMouseButtonDown(1) && DropApple.GetAppleCount() >=1 && PlayerSetUp.GetSoPhim() ==1&& PlayerSetUp.GetDangAnTao() == false)
        {
            RpcXuLiBanTao(Object.InputAuthority);
            Debug.Log("adsada");
        }
        
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RpcXuLiBanTao(PlayerRef playerRef)
    {
        NetworkObject tao = Runner.Spawn(TraiTaoRef, viTriBanTao.position, Quaternion.identity);
        Rigidbody rigidbody = tao.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.linearVelocity = viTriBanTao.forward * 5;
        }
        if (Runner.LocalPlayer == playerRef)
        {
            DropApple.GiamTaoDangCo();
        }

    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.StateAuthority)]
    public void RpcBanTao(PlayerRef playerRef)
    {
        
        
        Debug.Log("1 trai tao da xuat hien");
    }
}
