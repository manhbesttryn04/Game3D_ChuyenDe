using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Unity.Cinemachine;
using Unity.VisualScripting;
public struct NetWorkInput : INetworkStruct
{
    public float netInput;
    public float netSpeed;
}
public class PlayerMove : NetworkBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -20f;
    public float currentSpeed;
    public float vInput = 0f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private AudioSource runWalkSound;
    [SerializeField] public Animator animator;

    private float verticalVelocity = 0f;
    private float turnSmoothVelocity;
    private bool isWalkingSoundPlaying = false;

    //Cap nhat animator network
    [Networked, OnChangedRender(nameof(WalkAndRun))]
    public NetWorkInput NetInput { get; set; }
    [Networked, OnChangedRender(nameof(HandleJump))]
    public bool isJumppingNet { get; set; }
    [Networked]
    private TickTimer jumpAnimResetTimer { get; set; }


    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;
        MovePlayer();
        PlayRunWalkSound();
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
                isJumppingNet = true; // ✅ Trigger nhảy cho mọi client

                // Reset flag nếu muốn (bởi vì bool chỉ gọi OnChangedRender khi đổi giá trị)
                jumpAnimResetTimer = TickTimer.CreateFromSeconds(Runner, 0.3f);

            }
            if (jumpAnimResetTimer.Expired(Runner))
            {
                isJumppingNet = false;
            }
        }
        else
        {
            verticalVelocity += gravity * Runner.DeltaTime;

        }



    }

    void MovePlayer()
    {

        vInput = 0f;
        if (Input.GetKey(KeyCode.W)) vInput += 1f;
        if (Input.GetKey(KeyCode.S)) vInput -= 1f;


        if (Mathf.Abs(NetInput.netInput - vInput) > 0.01f)
        {
            var input = NetInput;
            input.netInput = vInput;
            NetInput = input;
        }


        Vector3 inputDir = new Vector3(0, 0, vInput);
        if (inputDir.magnitude >= 0.1f)
        {
            if (vInput > 0f)
            {
                float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
                transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
            }

            float tmoveAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;


            Vector3 moveDir = Quaternion.Euler(0f, tmoveAngle, 0f) * Vector3.forward;
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isRunning ? runSpeed : walkSpeed;

            Vector3 velocity = moveDir.normalized * currentSpeed;
            velocity.y = verticalVelocity;

            characterController.Move(velocity * Runner.DeltaTime); // ✅ dùng Runner.DeltaTime
        }
        else
        {
            Vector3 idleVelocity = new Vector3(0, verticalVelocity, 0);
            characterController.Move(idleVelocity * Runner.DeltaTime); // ✅ dùng Runner.DeltaTime
        }
        if (Mathf.Abs(NetInput.netSpeed - currentSpeed) > 0.01f)
        {
            var speednet = NetInput;
            speednet.netSpeed = currentSpeed;
            NetInput = speednet;
        }





    }
    public void WalkAndRun()
    {
        if (NetInput.netInput > 0)
        {
            animator.SetFloat("Run", NetInput.netSpeed);
            animator.SetFloat("RunBack", 0f);
        }
        else if (NetInput.netInput < 0)
        {

            animator.SetFloat("Run", 0f);
            animator.SetFloat("RunBack", NetInput.netSpeed);
        }
        else
        {
            animator.SetFloat("Run", 0f);
            animator.SetFloat("RunBack", 0f);
        }
    }

    public void HandleJump()
    {
        if (isJumppingNet == true)
        {
            if (animator.GetFloat("Run") > 0 || animator.GetFloat("RunBack") > 0)
            {
                animator.SetTrigger("JumpRun");

            }
            else { animator.SetTrigger("Jump"); }
        }



    }


    public void PlayRunWalkSound()
    {
        float speed = animator.GetFloat("Run");

        if (speed > 0 && !runWalkSound.isPlaying)
        {
            runWalkSound.pitch = speed >= runSpeed ? 1.8f : 1f;
            runWalkSound.Play();
            isWalkingSoundPlaying = true;
        }
        else if (speed == 0 && isWalkingSoundPlaying)
        {
            runWalkSound.Stop();
            isWalkingSoundPlaying = false;
        }
    }
    public void setCamera(Transform transform)
    {
        if (Object.HasInputAuthority) { cameraTransform = transform; }

    }


}
