//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private GameEvent _PauseEvent;

    [SerializeField]
    private IdContainer _IdProvider;

    [SerializeField]
    private float _Velocity;

    [Tooltip("Event invoked when the jump is started")]
    [SerializeField]
    private GameEvent _JumpStart;

    [Tooltip("Event invoked when the jump is completed with success")]
    [SerializeField]
    private GameEvent _JumpCompleted;

    [Tooltip("Event invoked when the jump is failed")]
    [SerializeField]
    private GameEvent _JumpFailed;


    private int _direction = 1;//1 = left, -1 = right


    private PlayerControls _gameplayInputProvider;
    private PlayerTouchController _playerTouchController;

    private Vector3 _movement;
    private bool _isMoving = false;
    private bool _isJumping = false;
    private bool _isPaused = false;

    private void Awake()
    {
        _gameplayInputProvider = PlayerController.Instance.GetInput<PlayerControls>(_IdProvider.Id);
        _playerTouchController = new PlayerTouchController();


    }

    private void OnEnable()
    {
        _playerTouchController.Enable();

        _gameplayInputProvider.OnTouch += StartMoving;
        _gameplayInputProvider.OnStopTouch += StopMoving;

        _PauseEvent.Subscribe(Pause);
        _JumpStart.Subscribe(JumpStarted);
        _JumpCompleted.Subscribe(JumpFinished);
        _JumpFailed.Subscribe(JumpFinished);
    }
    private void OnDisable()
    {
        _playerTouchController.Disable();
        _gameplayInputProvider.OnTouch -= StartMoving;
        _gameplayInputProvider.OnStopTouch -= StopMoving;

        _PauseEvent.Unsubscribe(Pause);
        _JumpStart.Unsubscribe(JumpStarted);
        _JumpCompleted.Unsubscribe(JumpFinished);
        _JumpFailed.Unsubscribe(JumpFinished);
    }

    private void Update()
    {
        if (_isMoving && !_isPaused && !_isJumping && Input.touchCount > 0)
        {
            MoveCharacter();
        }

        // if (Input.touchCount > 1)
        // {
        //     //Debug.Log("Touch Count: " + Input.touchCount);
        // }
    }

    private void StopMoving()
    {
        _isMoving = false;
        // Touch touch = Input.GetTouch(Input.touchCount - 1);
        // Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        // Debug.LogFormat("Touch Position End: {0}", touchPosition);
    }

    private void StartMoving()
    {
        _isMoving = true;
        // Touch touch = Input.GetTouch(Input.touchCount - 1);
        // Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        // Debug.LogFormat("Touch Position: {0}", touchPosition);
    }

    private void MoveCharacter()
    {
        // Vector2 position = _playerTouchController.Touch.PrimaryPosition.ReadValue<Vector2>();
        // Vector3 touchPosition = Camera.main.ScreenToWorldPoint(position);
        Touch touch = Input.GetTouch(Input.touchCount - 1);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //Debug.LogFormat("Touch Position: {0}", touchPosition);
        if (touchPosition.x < 0)
        {
            transform.position -= new Vector3(_Velocity * Time.deltaTime, 0, 0);
            if (_direction == -1)
            {
                _direction = 1;
                transform.localScale = new Vector3(_direction, 1, 1);
            }
        }
        else if (touchPosition.x > 0)
        {
            transform.position += new Vector3(_Velocity * Time.deltaTime, 0, 0);
            if (_direction == 1)
            {
                _direction = -1;
                transform.localScale = new Vector3(_direction, 1, 1);
            }
        }
        //_isMoving = true;
        //Debug.LogFormat("Value: {0}", value);

        if (transform.position.x > 4f || transform.position.x < -4f)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
    }


    private void Pause(GameEvent evt)
    {
        _isPaused = GameMaster.Instance.getPause();
    }

    private void JumpStarted(GameEvent evt)
    {
        _isJumping = true;
        //set collision layer
        gameObject.layer = 3;
    }

    private void JumpFinished(GameEvent evt)
    {
        _isJumping = false;
        //set collision layer
        gameObject.layer = 6;
    }
}
