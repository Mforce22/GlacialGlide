using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the spawning of avalanches in the game.
/// </summary>
public class SpawnAvalanche : MonoBehaviour {
    [SerializeField]
    private GameObject _AvalanchePrefab; // The prefab for the avalanche object.
    [SerializeField]
    private GameObject _AvalancheSpawnPoint; // The spawn point for the avalanche.

    [SerializeField]
    private float _StartingSecondsForSpawn; // The starting time interval for avalanche spawning.
    [SerializeField]
    private float _EndingSecondsForSpawn; // The ending time interval for avalanche spawning.

    private bool _IsAvalancheSpawned; // Indicates whether an avalanche is currently spawned.

    private void Start() {
        StartCoroutine(SpawnAvalancheRoutine(_StartingSecondsForSpawn, _EndingSecondsForSpawn));
    }

    private IEnumerator SpawnAvalancheRoutine(float start, float end) {
        while (true) {
            // Generate a random time interval for avalanche spawning.
            float randomTimeForSpawn = Random.Range(start, end);

            // Wait for the generated time interval.
            yield return new WaitForSeconds(randomTimeForSpawn);

            // Instantiate an avalanche at the spawn point.
            GameObject avalanche = Instantiate(_AvalanchePrefab, _AvalancheSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}