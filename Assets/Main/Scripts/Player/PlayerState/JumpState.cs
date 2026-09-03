using UnityEngine;

public class JumpState : PlayerState
{
    private float jumpForce = 6f;
    private float jumpStartTime;
    private float maxJumpDuration = 0.5f;

    public JumpState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        jumpStartTime = Time.time;
        player.AnimationController.SetJump(true);
        player.SetVelocityY(jumpForce);
        player.SetGrounded(false);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        // Движение в воздухе (опционально)
        Vector2 moveInput = player.InputHandler.MoveInput;
        if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            player.Move(player.WalkSpeed);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.ApplyGravity();
    }

    public override void Update()
    {
        base.Update();

        // Проверка на падение (когда персонаж начинает опускаться)
        if (player.Velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }

        // Проверка на максимальную длительность прыжка
        if (Time.time - jumpStartTime > maxJumpDuration)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.AnimationController.SetJump(false);
    }
}