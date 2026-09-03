using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    // Hash параметров для оптимизации
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int GroundedHash = Animator.StringToHash("Grounded");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int FreeFallHash = Animator.StringToHash("FreeFall");
    private static readonly int MotionSpeedHash = Animator.StringToHash("MotionSpeed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat(SpeedHash, speed);
        animator.SetFloat(MotionSpeedHash, speed / 6f);        
    }

    public void SetGrounded(bool grounded)
    {
        animator.SetBool(GroundedHash, grounded);
    }

    public void SetFreeFall(bool freeFall)
    {
        animator.SetBool(FreeFallHash, freeFall);
    }

    public void SetJump(bool jump)
    {
        animator.SetBool(JumpHash, jump);
    }

    public void OnLand()
    {
        // Устанавливаем состояние земли
        animator.SetBool(GroundedHash, true);
        animator.SetBool(FreeFallHash, false);
        animator.SetBool(JumpHash, false);        
    }
}