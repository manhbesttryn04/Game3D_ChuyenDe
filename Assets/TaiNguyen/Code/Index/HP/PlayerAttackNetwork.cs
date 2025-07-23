using Fusion;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerAttackNetwork : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;
    [SerializeField] NetworkHealth health;
    [Networked, OnChangedRender(nameof(AnimatorAttack))]
    public bool onAttack { get; set; }
    public TickTimer timer { get; set; }
    [Networked, OnChangedRender(nameof(BiSatThuong))]
    public float isKill { get; set; }
    [SerializeField] public CharacterController characterController;
    public float lastHP;



    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;
        if (characterController.isGrounded)
        {
            if (Input.GetMouseButtonDown(1))
            {
                onAttack = true;

                timer = TickTimer.CreateFromSeconds(Runner, 0.1f);
            }
            if (onAttack)
            {
                if (timer.ExpiredOrNotRunning(Runner) && timer.Expired(Runner))
                {
                    onAttack = false;

                    //Debug.Log("Da riset Danh");
                }
            }
        }

        isKill = health.GetHp();


    }
    public void AnimatorAttack()
    {
        if (onAttack)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("NamDam"))
        {
            health.TruHealth(1); // Gây sát thương mỗi frame
            Debug.Log("Bi gay sat thuong lien tuc");
        }
    }

    public void BiSatThuong()
    {
        animator.SetTrigger("IsKill");

        Debug.Log("Bi danh");


    }
}
