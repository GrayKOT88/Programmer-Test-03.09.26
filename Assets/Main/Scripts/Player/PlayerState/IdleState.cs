using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.SetSpeed(0);
        player.AnimationController.SetSpeed(0);
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

        // Проверка на движение
        Vector2 moveInput = player.InputHandler.MoveInput;
        if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            if (Mathf.Abs(moveInput.x) > 0.8f && player.IsSprintPressed)
            {
                stateMachine.ChangeState(stateMachine.RunState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.WalkState);
            }
        }
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