using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAvalanche : MonoBehaviour
{
    [SerializeField]
    private GameObject _DespawnPoint;

    private void Update() {
        if (transform.position.y <= _DespawnPoint.transform.position.y) {
            Destroy(gameObject);
        }
    }
}
