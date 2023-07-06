using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    #region variables
    [Header("Spawn Settings")]

    [SerializeField]
    [Tooltip("Trees spawn rate")]
    private int Trees_SpawnRate;
    [SerializeField]
    private GameObject Tree1Prefab;
    [SerializeField]
    private GameObject Tree2Prefab;
    [SerializeField]
    private GameObject Tree3Prefab;

    [SerializeField]
    [Tooltip("Bombs spawn rate")]
    private int Bombs_SpawnRate;
    [SerializeField]
    private GameObject BombPrefab;

    [SerializeField]
    [Tooltip("Coin spawn rate")]
    private int Coin_SpawnRate;
    [SerializeField]
    private GameObject CoinPrefab;

    [SerializeField]
    [Tooltip("Shield spawn rate")]
    private int Shield_SpawnRate;
    [SerializeField]
    private GameObject ShieldPrefab;

    [SerializeField]
    [Tooltip("X2 spawn rate")]
    private int X2_SpawnRate;
    [SerializeField]
    private GameObject X2Prefab;

    [SerializeField]
    [Tooltip("Heart spawn rate")]
    private int Heart_SpawnRate;
    [SerializeField]
    private GameObject HeartPrefab;

    [SerializeField]
    [Tooltip("Ramp spawn rate")]
    private int Ramp_SpawnRate;
    [SerializeField]
    [Tooltip("Small ramp spawn rate")]
    private int SRamp_SpawnRate;
    [SerializeField]
    [Tooltip("Medium ramp spawn rate")]
    private int MRamp_SpawnRate;
    [SerializeField]
    [Tooltip("Big ramp spawn rate")]
    private int LRamp_SpawnRate;
    [SerializeField]
    private GameObject SRampPrefab;
    [SerializeField]
    private GameObject MRampPrefab;
    [SerializeField]
    private GameObject LRampPrefab;

    [SerializeField]
    private GameObject SpawnPoint;
    #endregion

    private void Start() {
        StartCoroutine(SpawnCollectiblesCoroutine());
    }

    private IEnumerator SpawnCollectiblesCoroutine() {

        while (true) {
            //move the spawn point
            SpawnPoint.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);

            //wait 0.6 seconds
            yield return new WaitForSeconds(0.6f);
            int randomValue = Random.Range(0, 100);

            //60 spawn rate trees
            if(randomValue < Trees_SpawnRate) {
                SpawnTree();
            }
            //20 spawn rate coins
            else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate) && randomValue > Trees_SpawnRate) {
                SpawnCoin();
            }
            //6 spawn rate bombs
            else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate) && randomValue > (Trees_SpawnRate + Coin_SpawnRate)) {
                SpawnBomb();
            }
            //6 spawn rate ramps
            else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate) && randomValue > (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate)) {
                SpawnRamp();
            }
            //4 spawn rate shield
            else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate) && randomValue > (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate)) {
                SpawnShield();
            }
            //2 spawn rate X2
            else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate + X2_SpawnRate) && randomValue > (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate)) {
                SpawnX2();
            }
            //2 spawn rate Heart
            /*else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate + X2_SpawnRate + Heart_SpawnRate) && randomValue > (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate + X2_SpawnRate)) {
                SpawnHeart();
            }*/
        }
    }

    private void SpawnTree() {
        int randomIndex = Random.Range(1, 4);
        GameObject treePrefab = null;

        switch (randomIndex) {
            case 1:
                treePrefab = Tree1Prefab;
                break;
            case 2:
                treePrefab = Tree2Prefab;
                break;
            case 3:
                treePrefab = Tree3Prefab;
                break;
        }

        if (treePrefab != null) {
            Instantiate(treePrefab, SpawnPoint.transform.position, Quaternion.identity);
        }
    }
    private void SpawnBomb() {
        Instantiate(BombPrefab, SpawnPoint.transform.position, Quaternion.identity);
    }
    private void SpawnCoin() {
        Instantiate(CoinPrefab, SpawnPoint.transform.position, Quaternion.identity);
    }
    private void SpawnShield() {
        Instantiate(ShieldPrefab, SpawnPoint.transform.position, Quaternion.identity);
    }
    private void SpawnX2() {
        Instantiate(X2Prefab, SpawnPoint.transform.position, Quaternion.identity);
    }
    private void SpawnHeart() {
        Instantiate(HeartPrefab, SpawnPoint.transform.position, Quaternion.identity);
    }
    private void SpawnRamp() {
        GameObject rampPrefab = null;

        if (Random.Range(0, 100) < SRamp_SpawnRate) {
            rampPrefab = SRampPrefab;
        }
        if (Random.Range(0, 100) < MRamp_SpawnRate) {
            rampPrefab = MRampPrefab;
        }
        if (Random.Range(0, 100) < LRamp_SpawnRate) {
            rampPrefab = MRampPrefab;
        }

        if (rampPrefab != null) {
            Instantiate(rampPrefab, SpawnPoint.transform.position, Quaternion.identity);
        }
    }

}
