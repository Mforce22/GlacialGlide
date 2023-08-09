using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the despawning of avalanches when they reach a certain point.
/// </summary>
public class DespawnAvalanche : MonoBehaviour {
    [SerializeField]
    private GameObject _DespawnPoint; // The point at which the avalanche should be despawned.

    private void Update() {
        // Check if the avalanche has reached or passed the despawn point.
        if (transform.position.y <= _DespawnPoint.transform.position.y) {
            // Destroy the avalanche GameObject.
            Destroy(gameObject);
        }
    }
}