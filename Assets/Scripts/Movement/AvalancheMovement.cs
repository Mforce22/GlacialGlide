using UnityEngine;

/// <summary>
/// Handles the movement behavior of an avalanche object, including speed changes triggered by events.
/// </summary>
public class AvalancheMovement : MonoBehaviour {
    [Header("Events")]
    [Tooltip("Event to subscribe to change the speed of the object")]
    [SerializeField]
    private GameEvent _ChangeSpeedEvent;

    [Header("Movement")]
    private float _movement;

    /// <summary>
    /// Subscribe to the speed change event during Awake.
    /// </summary>
    private void Awake() {
        _ChangeSpeedEvent.Subscribe(SpeedChanged);
    }

    /// <summary>
    /// Unsubscribe from the speed change event when the object is disabled.
    /// </summary>
    private void OnDisable() {
        _ChangeSpeedEvent.Unsubscribe(SpeedChanged);
    }

    /// <summary>
    /// Start is called before the first frame update. Initialize movement speed and subscribe to the speed change event.
    /// </summary>
    void Start() {
        // Initialize the movement speed with the current velocity from the GameMaster.
        _movement = GameMaster.Instance.getVelocity();

        // Subscribe to the speed change event.
        _ChangeSpeedEvent.Subscribe(SpeedChanged);
    }

    /// <summary>
    /// Update is called once per frame and handles object movement.
    /// </summary>
    void Update() {
        // If the game isn't paused, move the avalanche object downward with the current movement speed.
        if (!GameMaster.Instance.getPause()) {
            transform.Translate(Vector3.down * _movement * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handles the speed change triggered by an event.
    /// </summary>
    /// <param name="evt">The event associated with the speed change.</param>
    void SpeedChanged(GameEvent evt) {
        // Update the movement speed with the new velocity from the GameMaster.
        _movement = GameMaster.Instance.getVelocity();

        // Log the speed change.
        Debug.Log("Speed changed to " + _movement);
    }
}