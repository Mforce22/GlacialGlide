using UnityEngine;

/// <summary>
/// An abstract base class for creating Singleton MonoBehaviours.
/// </summary>
/// <typeparam name="T">The derived Singleton class.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    /// <summary>
    /// The instance of the Singleton.
    /// </summary>
    public static T Instance {
        get;
        private set;
    }

    /// <summary>
    /// Called when the Singleton MonoBehaviour is awakened.
    /// </summary>
    private void Awake() {
        if (Instance != null && Instance != this) {
            // If an instance already exists, destroy this GameObject.
            Destroy(gameObject);
        } else {
            // Otherwise, set this instance as the Singleton.
            Instance = (T)this;
        }
        OnAwake();
    }

    /// <summary>
    /// A virtual method that can be overridden to provide additional logic in Awake.
    /// </summary>
    protected virtual void OnAwake() {
        // This method is intentionally left empty for derived classes to override.
    }
}