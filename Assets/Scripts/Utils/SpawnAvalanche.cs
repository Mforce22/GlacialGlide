using System.Collections;
using UnityEngine;

public class SpawnAvalanche : MonoBehaviour {
    [SerializeField]
    private GameObject _AvalanchePrefab;
    [SerializeField]
    private GameObject _AvalancheSpawnPoint;

    private bool _IsAvalancheSpawned;

    private void Start() {
        StartCoroutine(SpawnAvalancheRoutine());
    }

    private IEnumerator SpawnAvalancheRoutine() {
        while (true) {
            float randomTimeForSpawn = Random.Range(60.0f, 120.0f);
            yield return new WaitForSeconds(randomTimeForSpawn);
            GameObject avalanche = Instantiate(_AvalanchePrefab, _AvalancheSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
