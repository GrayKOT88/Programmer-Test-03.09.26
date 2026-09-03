using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;    
    [SerializeField] private float gravity = -9.81f;    

    // Компоненты
    private CharacterController characterController;
    private PlayerInputHandler inputHandler;
    private PlayerAnimationController animationController;
    private PlayerStateMachine stateMachine;

    // Состояние игрока
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;

    // Публичные свойства для состояний
    public CharacterController CharacterController => characterController;
    public PlayerInputHandler InputHandler => inputHandler;
    public PlayerAnimationController AnimationController => animationController;
    public PlayerStateMachine StateMachine => stateMachine;
    public Vector3 Velocity => velocity;
    public bool IsGrounded => isGrounded;
    public bool IsSprintPressed => inputHandler.SprintPressed;
    public float WalkSpeed => moveSpeed * 0.5f;
    public float RunSpeed => moveSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();
        animationController = GetComponentInChildren<PlayerAnimationController>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        HandleGravity();
        UpdateGroundedState();
    }

    private void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void UpdateGroundedState()
    {
        isGrounded = characterController.isGrounded;
        animationController?.SetGrounded(isGrounded);
    }

    // Методы для состояний
    public void Move(float speed)
    {
        Vector2 moveInput = inputHandler.MoveInput;
        Vector3 moveDirection = new Vector3(moveInput.x, 0, 0);

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = moveDirection.x > 0 ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

        characterController.Move(moveDirection * speed * Time.deltaTime);
        //currentSpeed = speed * Mathf.Abs(moveInput.x);
        //animationController?.SetSpeed(currentSpeed);
        animationController?.SetSpeed(speed);
    }

    public void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void SetVelocityY(float value)
    {
        velocity.y = value;
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
        animationController?.SetGrounded(grounded);
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
        animationController?.SetSpeed(speed);
    }
}