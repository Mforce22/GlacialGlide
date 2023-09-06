using UnityEngine;

/// <summary>
/// Manages the restart behavior for a GameObject positioned on a slope.
/// </summary>
public class SlopeRestart : MonoBehaviour {
    [SerializeField]
    private GameObject _targetSlope;

    [SerializeField]
    private GameObject _slopeEnd;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update() {
        // Check if the y position is greater than or equal to the slope end's y position.
        if (transform.position.y >= _slopeEnd.transform.position.y) {
            // Move the GameObject to the slope's starting position with an offset.
            transform.position = new Vector3(transform.position.x, (_targetSlope.transform.position.y - 9.6f), transform.position.z);
        }
    }
}
