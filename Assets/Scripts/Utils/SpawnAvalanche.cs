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

    [SerializeField]
    private GameObject _WarningUIPrefab; // The prefab for the warning UI.
    [SerializeField]
    private float _WarningUIDisplayTime; // The time for which the warning UI is displayed.

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
            Instantiate(_AvalanchePrefab, _AvalancheSpawnPoint.transform.position, Quaternion.identity);
            GameObject warningUI = Instantiate(_WarningUIPrefab);
            warningUI.GetComponentInChildren<CanvasGroup>().alpha = 0.0f;
            for (int i = 0; i < 5; i++) {
                warningUI.GetComponentInChildren<CanvasGroup>().alpha += 0.2f;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(_WarningUIDisplayTime);
            for (int i = 0; i < 5; i++) {
                warningUI.GetComponentInChildren<CanvasGroup>().alpha -= 0.2f;
                yield return new WaitForSeconds(0.1f);
            }
            Destroy(warningUI);

        }
    }
}