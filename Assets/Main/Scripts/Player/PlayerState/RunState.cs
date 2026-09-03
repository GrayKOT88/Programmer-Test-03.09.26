using UnityEngine;

public class RunState : PlayerState
{
    private float runSpeed = 6f;

    public RunState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.AnimationController.SetSpeed(runSpeed);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        // Проверка на прыжок
        if (player.InputHandler.JumpPressed && player.IsGrounded)
        {
            stateMachine.ChangeState(stateMachine.JumpState);
            return;
        }

        // Проверка на остановку
        Vector2 moveInput = player.InputHandler.MoveInput;
        if (Mathf.Abs(moveInput.x) < 0.1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // Проверка на ходьбу
        if (Mathf.Abs(moveInput.x) <= 0.8f || !player.IsSprintPressed)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Move(runSpeed);
    }

    public override void Update()
    {
        base.Update();

        // Проверка на падение
        if (!player.IsGrounded && player.Velocity.y < -0.1f)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }
    }
}