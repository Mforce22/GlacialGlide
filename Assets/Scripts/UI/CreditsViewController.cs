using UnityEngine;

/// <summary>
/// This class manages the credits view and provides a method to close it.
/// </summary>
public class CreditsViewController : MonoBehaviour {
    /// <summary>
    /// This method is called to close the credits view and destroy the GameObject.
    /// </summary>
    public void CloseCredits() {
        // Destroy the credits view GameObject.
        Destroy(gameObject);
    }
}