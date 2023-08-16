
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private GPSInput playerControls = null;
    private Vector2 moveVector = Vector2.zero;
    [SerializeField]
    private GameObject rb = null;
    [SerializeField]
    private float _Velocity;


    private int _direction = 1;//1 = left, -1 = right

    private void Awake()
    {
        playerControls = new GPSInput();
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
    private void Update()
    {
        if (moveVector.x < 0)
        {
            rb.transform.position -= new Vector3(_Velocity * Time.deltaTime, 0, 0);
            if (_direction == -1)
            {
                _direction = 1;
                rb.transform.localScale = new Vector3(_direction, 1, 1);
            }
        }
        else if (moveVector.x > 0)
        {
            rb.transform.position += new Vector3(_Velocity * Time.deltaTime, 0, 0);
            if (_direction == 1)
            {
                _direction = -1;
                rb.transform.localScale = new Vector3(_direction, 1, 1);
            }
        }
    }
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

    }
}