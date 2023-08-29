using UnityEngine;

public class AvalancheMovement : MonoBehaviour {
    [Header("Events")]
    [Tooltip("Event to subscribe to change the speed of the object")]
    [SerializeField]
    private GameEvent _ChangeSpeedEvent;

    [Header("Movement")]
    private float _movement;

    private void Awake() {
        _ChangeSpeedEvent.Subscribe(SpeedChanged);
    }

    private void OnDisable() {
        _ChangeSpeedEvent.Unsubscribe(SpeedChanged);
    }


    // Start is called before the first frame update
    void Start() {
        //get instance of GameMaster
        _movement = GameMaster.Instance.getVelocity();

        //used for testing
        _ChangeSpeedEvent.Subscribe(SpeedChanged);
    }

    // Update is called once per frame
    void Update() {
        //if the game isn't in pause
        if (!GameMaster.Instance.getPause()) {
            //move up 
            transform.Translate(Vector3.down * _movement * Time.deltaTime);
        }
    }

    void SpeedChanged(GameEvent evt) {
        _movement = GameMaster.Instance.getVelocity();
        Debug.Log("Speed changed to " + _movement);
    }
}
