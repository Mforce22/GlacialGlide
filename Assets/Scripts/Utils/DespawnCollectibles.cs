using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the despawning of collectibles when they reach a certain point.
/// </summary>
public class DespawnCollectibles : MonoBehaviour {
    [SerializeField]
    private GameObject _slopeEnd; // The point at which the collectible should be despawned.

    [SerializeField]
    private IdContainer _idPoolManager; // The IdContainer for the associated PoolManager.

    private void Update() {
        // Check if the y position is greater than or equal to the slope end point.
        if (transform.position.y >= _slopeEnd.transform.position.y) {
            // Return the collectible to the object pool for reuse.
            PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_idPoolManager);
            poolManager.ReturnPoolableObject(gameObject.GetComponent<PoolableObject>());
        }
    }
}