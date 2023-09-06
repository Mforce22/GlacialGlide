using UnityEngine;

/// <summary>
/// This script ensures that the GameObject it is attached to will not be destroyed when loading new scenes in Unity.
/// </summary>
public class DontDestroy : MonoBehaviour {
    /// <summary>
    /// This method is called when the GameObject this script is attached to is created.
    /// It marks the GameObject as "Don't Destroy On Load," ensuring it persists across scene changes.
    /// </summary>
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}