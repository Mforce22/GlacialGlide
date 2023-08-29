using UnityEngine;

public class TreeCollisionDetector : MonoBehaviour {

    [Header("Events")]
    [Tooltip("Event to invoke when the object hit the player")]
    [SerializeField]
    private GameEvent _EventToInvoke;

    private bool _haveAlreadyDamaged = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!_haveAlreadyDamaged) {
            _haveAlreadyDamaged = true;
            Debug.Log("Trigger detected " + gameObject.name + " on " + other.gameObject.name);
            _EventToInvoke.Invoke();
        }

    }
}
