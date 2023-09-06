using System.Collections;
using UnityEngine;

/// <summary>
/// A class responsible for spawning avalanches at specified intervals.
/// </summary>
public class SpawnAvalanche : MonoBehaviour {
    [SerializeField]
    private GameObject _AvalanchePrefab; 
    [SerializeField]
    private GameObject _AvalancheSpawnPoint; 

    [SerializeField]
    private float _StartingSecondsForSpawn; 
    [SerializeField]
    private float _EndingSecondsForSpawn; 

    [SerializeField]
    private GameObject _WarningUIPrefab; 
    [SerializeField]
    private float _WarningUIDisplayTime;

    /// <summary>
    /// Starts the spawning routine for avalanches at specified intervals.
    /// </summary>
    private void Start() {
        StartCoroutine(SpawnAvalancheRoutine(_StartingSecondsForSpawn, _EndingSecondsForSpawn));
    }

    /// <summary>
    /// A coroutine responsible for spawning avalanches at random intervals and displaying warning UI.
    /// </summary>
    /// <param name="start">The minimum time interval for spawning avalanches.</param>
    /// <param name="end">The maximum time interval for spawning avalanches.</param>
    private IEnumerator SpawnAvalancheRoutine(float start, float end) {
        while (true) {
            // Generate a random time interval for the next avalanche spawn.
            float randomTimeForSpawn = Random.Range(start, end);

            // Wait for the random time interval.
            yield return new WaitForSeconds(randomTimeForSpawn);

            // Instantiate an avalanche at the specified spawn point.
            Instantiate(_AvalanchePrefab, _AvalancheSpawnPoint.transform.position, Quaternion.identity);

            // Instantiate a warning UI and gradually fade it in.
            GameObject warningUI = Instantiate(_WarningUIPrefab);
            warningUI.GetComponentInChildren<CanvasGroup>().alpha = 0.0f;
            for (int i = 0; i < 5; i++) {
                warningUI.GetComponentInChildren<CanvasGroup>().alpha += 0.2f;
                yield return new WaitForSeconds(0.1f);
            }

            // Wait for the specified time to display the warning UI.
            yield return new WaitForSeconds(_WarningUIDisplayTime);

            // Gradually fade out and destroy the warning UI.
            for (int i = 0; i < 5; i++) {
                warningUI.GetComponentInChildren<CanvasGroup>().alpha -= 0.2f;
                yield return new WaitForSeconds(0.1f);
            }
            Destroy(warningUI);
        }
    }
}