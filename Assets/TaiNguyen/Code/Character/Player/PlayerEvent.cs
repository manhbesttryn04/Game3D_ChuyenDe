using Fusion;
using UnityEngine;

public class PlayerEvent : NetworkBehaviour
{
    [SerializeField] private BoxCollider namDam;
    public void BatDauTanCong()
    {
        // Debug.Log("Bat dau tan cong ");
        namDam.enabled = true;
    }
    public void KetThucTanCong()
    {
        //Debug.Log("Ket thuc tan cong");
        namDam.enabled = false;
    }
}
