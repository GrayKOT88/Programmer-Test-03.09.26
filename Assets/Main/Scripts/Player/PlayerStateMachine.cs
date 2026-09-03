using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState { get; private set; }

    // Все состояния
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public LandState LandState { get; private set; }

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();

        // Инициализация состояний
        IdleState = new IdleState(player, this);
        WalkState = new WalkState(player, this);
        RunState = new RunState(player, this);
        JumpState = new JumpState(player, this);
        FallState = new FallState(player, this);
        LandState = new LandState(player, this);
    }

    private void Start()
    {
        // Начальное состояние
        ChangeState(IdleState);
    }

    private void Update()
    {
        CurrentState?.Update();
        CurrentState?.HandleInput();
    }

    private void FixedUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        PreviousState = CurrentState;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();

        Debug.Log($"State changed from {PreviousState?.GetType().Name} to {CurrentState?.GetType().Name}");
    }

    public void RevertToPreviousState()
    {
        if (PreviousState != null)
        {
            ChangeState(PreviousState);
        }
    }
}