using UnityEngine;

public class LandState : PlayerState
{
    private float landDuration = 0.2f;
    private float landStartTime;

    public LandState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        landStartTime = Time.time;
        player.SetGrounded(true);
        player.SetVelocityY(0);        
    }

    public override void Update()
    {
        base.Update();

        // После короткой задержки переходим в Idle или Walk
        if (Time.time - landStartTime >= landDuration)
        {
            Vector2 moveInput = player.InputHandler.MoveInput;
            if (Mathf.Abs(moveInput.x) > 0.1f)
            {
                stateMachine.ChangeState(stateMachine.WalkState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.AnimationController.SetGrounded(true);
    }
}