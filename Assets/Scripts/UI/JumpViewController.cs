using System.Collections;
using UnityEngine;

public class JumpViewController : MonoBehaviour
{
    #region Events
    [Tooltip("Event to invoke when the jump is completed with success")]
    [SerializeField]
    private GameEvent _JumpCompleted;

    [Tooltip("Event to invoke when the jump is failed")]
    [SerializeField]
    private GameEvent _JumpFailed;
    #endregion
    #region Variables
    [SerializeField]
    private GameObject _RedCircle;

    private Vector3 _vect = new Vector3(0.04f, 0.04f, 0);
    private float oldSpeed;
    #endregion

    /// <summary>
    /// Initiates a reduction in the game's speed and logs the action.
    /// </summary>
    private void Start() {
        // Store the current game speed.
        oldSpeed = GameMaster.Instance.getVelocity();

        // Set the game speed to a reduced value.
        GameMaster.Instance.setVelocity(0.5f);

        // Log that the speed has been reduced.
        Debug.Log("Speed reduced");

        // Start a coroutine to gradually restore the original speed.
        StartCoroutine(Reduce());
    }

    /// <summary>
    /// Gradually reduces the size of a red circle and triggers a "Jump Failed" event when it reaches a certain size.
    /// </summary>
    private IEnumerator Reduce() {
        // Determine the jump difficulty levels available to the player.
        bool _canEasyJump = GameMaster.Instance.getEasyJump();
        bool _canMediumJump = GameMaster.Instance.getMediumJump();
        bool _canHardJump = GameMaster.Instance.getHardJump();

        // Reduce the size of the red circle until it reaches a certain size.
        while (_RedCircle.transform.localScale.x >= 0.5f) {
            _RedCircle.transform.localScale = _RedCircle.transform.localScale - _vect;

            // Depending on the jump difficulty, wait for different intervals.
            if (_canEasyJump) {
                yield return new WaitForSeconds(0.1f);
            } else if (_canMediumJump) {
                yield return new WaitForSeconds(0.08f);
            } else if (_canHardJump) {
                yield return new WaitForSeconds(0.06f);
            }
        }

        // Log that the jump has failed and trigger the "Jump Failed" event.
        Debug.Log("JUMP FAILED");
        _JumpFailed.Invoke();

        // Destroy the GameObject associated with this coroutine.
        Destroy(gameObject);
    }

    [ContextMenu("Click debug")]
    /// <summary>
    /// Handles the player's click input, determining whether a jump is successful or failed.
    /// </summary>
    public void Click() {
        // Check if the size of the red circle falls within the acceptable range for a successful jump.
        if (_RedCircle.transform.localScale.x >= 0.6f && _RedCircle.transform.localScale.x <= 1.1f) {
            // Log that the jump has been successfully completed.
            Debug.Log("JUMP COMPLETED");

            // Trigger the "Jump Completed" event.
            _JumpCompleted.Invoke();

            // Destroy the GameObject associated with this action.
            Destroy(gameObject);
        } else {
            // Log that the jump has failed.
            Debug.Log("JUMP FAILED");

            // Trigger the "Jump Failed" event.
            _JumpFailed.Invoke();

            // Destroy the GameObject associated with this action.
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Restores the game's speed to its previous value when the GameObject is disabled.
    /// </summary>
    private void OnDisable() {
        // Log that the game speed is being restored.
        Debug.Log("Speed restored");

        // Restore the game's speed to its previous value.
        GameMaster.Instance.setVelocity(oldSpeed);
    }
}
