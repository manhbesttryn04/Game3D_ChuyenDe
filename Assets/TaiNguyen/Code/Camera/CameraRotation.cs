using Fusion;
using UnityEngine;

public class CameraRotation : NetworkBehaviour
{
    public float vitriXoatTrai = -45f;
    public float vitriXoayPhai = 45f;
    public float tocDoXoay = 2f;

    public  bool quaySangPhai = true;

    public override void FixedUpdateNetwork()


    { tocDoXoay = Runner.DeltaTime *0.3f;
        float gocY = transform.rotation.eulerAngles.y;
        if (gocY > 180f) gocY -= 360f; // Convert về khoảng -180 đến 180

        // Đổi hướng nếu đạt vị trí giới hạn
        if (quaySangPhai && gocY >= vitriXoayPhai - 1f)
        {
            quaySangPhai = false;
        }
        else if (!quaySangPhai && gocY <= vitriXoatTrai + 1f)
        {
            quaySangPhai = true;
        }   

        // Xác định góc mục tiêu
        float gocMucTieu = quaySangPhai ? vitriXoayPhai : vitriXoatTrai;

        // Xoay mượt đến góc mục tiêu
        Quaternion dich = Quaternion.Euler(transform.rotation.x, gocMucTieu, transform.rotation.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, dich, tocDoXoay );
    }
}
