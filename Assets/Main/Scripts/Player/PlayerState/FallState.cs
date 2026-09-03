using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.AnimationController.SetFreeFall(true);
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

        // Проверка на приземление
        if (player.IsGrounded)
        {
            stateMachine.ChangeState(stateMachine.LandState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.AnimationController.SetFreeFall(false);
    }
}