
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private GPSInput playerControls = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb= null;

    [SerializeField]
    private float moveSpeed = 10f;
    private void Awake()
    {
        playerControls = new GPSInput();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Touch.Movement.performed += OnMovementPerformed;
        playerControls.Touch.Movement.canceled += OnMovementCancelled;
    }


    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Touch.Movement.performed -= OnMovementPerformed;
        playerControls.Touch.Movement.canceled -= OnMovementCancelled;
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector= value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled (InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

    }
}