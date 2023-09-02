using UnityEngine;

public class HowToPlayViewController : MonoBehaviour {
    /// <summary>
    /// Closes the "How to Play" view by destroying the associated GameObject.
    /// </summary>
    public void CloseView() {
        // Destroy the GameObject associated with the "How to Play" view to close it.
        Destroy(gameObject);
    }
}