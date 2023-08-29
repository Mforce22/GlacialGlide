using UnityEngine;

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


    private void OnEnable() {
        _onShieldTaken.Subscribe(OnShieldTaken);
        _onShieldRemoved.Subscribe(OnShieldRemoved);
    }

    private void OnDisable() {
        _onShieldTaken.Unsubscribe(OnShieldTaken);
        _onShieldRemoved.Unsubscribe(OnShieldRemoved);
    }

    private void OnShieldTaken(GameEvent evt) {
        _shield.SetActive(true);
    }

    private void OnShieldRemoved(GameEvent evt) {
        _shield.SetActive(false);
    }

}
