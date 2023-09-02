using UnityEngine;

/// <summary>
/// Manages the behavior of the shield power-up in the game.
/// </summary>
public class ShieldManager : MonoBehaviour {
    [Tooltip("The shield prefab")]
    [SerializeField]
    private GameObject _shield;

    [Tooltip("Event called when the shield is taken")]
    [SerializeField]
    private GameEvent _onShieldTaken;

    [Tooltip("Event called when the shield is removed")]
    [SerializeField]
    private GameEvent _onShieldRemoved;

    /// <summary>
    /// Subscribes to events when the shield manager is enabled.
    /// </summary>
    private void OnEnable() {
        _onShieldTaken.Subscribe(OnShieldTaken);
        _onShieldRemoved.Subscribe(OnShieldRemoved);
    }

    /// <summary>
    /// Unsubscribes from events when the shield manager is disabled.
    /// </summary>
    private void OnDisable() {
        _onShieldTaken.Unsubscribe(OnShieldTaken);
        _onShieldRemoved.Unsubscribe(OnShieldRemoved);
    }

    /// <summary>
    /// Activates the shield when it is taken.
    /// </summary>
    /// <param name="evt">The event associated with the shield being taken.</param>
    private void OnShieldTaken(GameEvent evt) {
        // Activate the shield GameObject.
        _shield.SetActive(true);
    }

    /// <summary>
    /// Deactivates the shield when it is removed or expires.
    /// </summary>
    /// <param name="evt">The event associated with the shield being removed or expiring.</param>
    private void OnShieldRemoved(GameEvent evt) {
        // Deactivate the shield GameObject.
        _shield.SetActive(false);
    }

}
