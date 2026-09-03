using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Values")]
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool SprintPressed { get; private set; }

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // —читываем ввод
        MoveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        JumpPressed = playerInput.actions["Jump"].WasPressedThisFrame();
        SprintPressed = playerInput.actions["Sprint"].IsPressed();
    }
}