using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _IdProvider;

    [SerializeField]
    private GameEvent _pauseEvent;

    [SerializeField]
    private GameEvent _jumpStartEvent;

    [SerializeField]
    private GameObject _minigamePrefab;


    [Header("Swipe Settings")]
    [SerializeField]
    private float _swipeDistance;

    [SerializeField]
    private float _maxSwipeTime;

    [SerializeField, Range(0f, 1f)]
    private float _directionTreshold = 0.9f;


    private PlayerControls _gameplayInputProvider;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float _startTime;
    private float _endTime;

    private bool _isPaused = false;

    /// <summary>
    /// This method is called when the object is initialized, typically before the first frame.
    /// It sets up the input provider for gameplay controls.
    /// </summary>
    private void Awake() {
        // Obtain the input provider for the specific player using their unique identifier.
        // The input provider allows the player to control the gameplay.
        _gameplayInputProvider = PlayerController.Instance.GetInput<PlayerControls>(_IdProvider.Id);

    }


    /// <summary>
    /// This method is called when the object is enabled, allowing it to reactivate its functionality.
    /// It sets up input events for gameplay controls and subscribes to the pause event.
    /// </summary>
    private void OnEnable() {

        // Obtain the input provider for the specific player using their unique identifier.
        // The input provider allows the player to control the gameplay.
        _gameplayInputProvider = PlayerController.Instance.GetInput<PlayerControls>(_IdProvider.Id);

        // Subscribe to touch-related events for player movement.
        _gameplayInputProvider.OnTouch += StartMoving;
        _gameplayInputProvider.OnStopTouch += StopMoving;

        // Subscribe to the pause event to handle pausing functionality.
        _pauseEvent.Subscribe(Pause);
    }
    
    /// <summary>
    /// This method is called when the object is disabled, allowing it to deactivate its functionality.
    /// It unsubscribes from input events for gameplay controls and the pause event.
    /// </summary>
    private void OnDisable() {

        // Unsubscribe from touch-related events for player movement.
        _gameplayInputProvider.OnTouch -= StartMoving;
        _gameplayInputProvider.OnStopTouch -= StopMoving;

        // Unsubscribe from the pause event to stop handling pausing functionality.
        _pauseEvent.Unsubscribe(Pause);
    }


    /// <summary>
    /// Initiates the player's movement in response to a touch input.
    /// Records the touch's starting position and time for velocity calculation.
    /// </summary>
    private void StartMoving() {
        // Obtain the latest touch input.
        Touch touch = Input.GetTouch(Input.touchCount - 1);

        // Convert the touch position to a world point using the main camera.
        startPosition = Camera.main.ScreenToWorldPoint(touch.position);

        // Record the time when the touch movement begins.
        _startTime = Time.time;
    }

    /// <summary>
    /// Stops the player's movement in response to the release of touch input.
    /// Records the touch's ending position and time for velocity calculation.
    /// Checks for a swipe gesture and initiates a jump if conditions are met.
    /// </summary>
    private void StopMoving() {
        // Obtain the latest touch input.
        Touch touch = Input.GetTouch(Input.touchCount - 1);

        // Convert the touch position to a world point using the main camera.
        endPosition = Camera.main.ScreenToWorldPoint(touch.position);

        // Record the time when the touch movement ends.
        _endTime = Time.time;

        // Check if the game is not paused.
        if (!_isPaused) {
            // Check for a swipe gesture.
            bool check = CheckSwipe();

            if (check) {
                // Check if the player can perform a jump.
                bool canJump = GameMaster.Instance.canJump();

                if (canJump) {
                    // Instantiate the minigame prefab and initiate a jump.
                    Instantiate(_minigamePrefab);
                    _jumpStartEvent.Invoke();
                } else {
                    Debug.Log("Can't Jump");
                }
            }
        } else {
            Debug.Log("Game is paused in swipe controller");
        }
    }

    /// <summary>
    /// Toggles the game's pause state in response to a pause event.
    /// </summary>
    /// <param name="evt">The pause event that triggers the pause or resume action.</param>
    private void Pause(GameEvent evt) {
        // Toggle the game's pause state.
        _isPaused = !_isPaused;
    }

    /// <summary>
    /// Checks for a swipe gesture based on touch input and specified criteria.
    /// </summary>
    /// <returns>True if a valid swipe gesture is detected, otherwise false.</returns>
    private bool CheckSwipe() {
        Debug.Log("Swipe check");

        // Calculate the distance between the starting and ending touch positions.
        float swipeDistance = Vector3.Distance(startPosition, endPosition);

        // Check if the swipe distance exceeds the specified threshold and the swipe duration is within limits.
        if (swipeDistance >= _swipeDistance && (_endTime - _startTime) <= _maxSwipeTime) {
            Debug.Log("Swipe Detected");

            // Calculate the direction of the swipe.
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;

            // Check if the swipe direction is predominantly upward.
            if (Vector2.Dot(direction2D, Vector2.up) > _directionTreshold) {
                Debug.Log("Swipe Up");
                return true;
            }
        }

        return false;
    }
}
