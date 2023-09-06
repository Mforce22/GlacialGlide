using System.Collections;
using UnityEngine;

/// <summary>
/// This script handles the cyclic movement of a GameObject along the x-axis.
/// </summary>
public class AvalancheMovePoint : MonoBehaviour {
    /// <summary>
    /// This method is called on script start.
    /// It initiates the coroutine for cyclic movement of the object.
    /// </summary>
    void Start() {
        StartCoroutine(MovePoint());
    }

    /// <summary>
    /// This coroutine cyclically moves the object along the x-axis by reversing its position
    /// every 10 seconds.
    /// </summary>
    IEnumerator MovePoint() {
        while (true) {
            // Reverse the position along the x-axis of the object
            transform.position = new Vector3((transform.position.x * (-1)), transform.position.y, transform.position.z);

            // Wait for 10 seconds before looping again
            yield return new WaitForSeconds(10.0f);
        }
    }
}