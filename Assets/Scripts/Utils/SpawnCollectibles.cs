using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    #region variables
    [Header("Spawn Settings")]

    [SerializeField]
    [Tooltip("Seconds of spawn rate")]
    private float SpawnRate;

    [SerializeField]
    [Tooltip("Trees spawn rate")]
    private int Trees_SpawnRate;
    [SerializeField]
    private IdContainer Tree1ManagerContainer;
    [SerializeField]
    private IdContainer Tree2ManagerContainer;
    [SerializeField]
    private IdContainer Tree3ManagerContainer;

    [SerializeField]
    [Tooltip("Bombs spawn rate")]
    private int Bombs_SpawnRate;
    [SerializeField]
    private IdContainer BombManagerContainer;

    [SerializeField]
    [Tooltip("Coin spawn rate")]
    private int Coin_SpawnRate;
    [SerializeField]
    private IdContainer CoinManagerContainer;

    [SerializeField]
    [Tooltip("Shield spawn rate")]
    private int Shield_SpawnRate;
    [SerializeField]
    private IdContainer ShieldManagerContainer;

    [SerializeField]
    [Tooltip("X2 spawn rate")]
    private int X2_SpawnRate;
    [SerializeField]
    private IdContainer X2ManagerContainer;

    [SerializeField]
    [Tooltip("Heart spawn rate")]
    private int Heart_SpawnRate;
    [SerializeField]
    private IdContainer HeartManagerContainer;

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
    private IdContainer SRampManagerContainer;
    [SerializeField]
    private IdContainer MRampManagerContainer;
    [SerializeField]
    private IdContainer LRampManagerContainer;

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
            yield return new WaitForSeconds(SpawnRate);

            if (!GameMaster.Instance.getPause()) {
                int randomValue = Random.Range(0, 100);

                //60 spawn rate trees
                if (randomValue < Trees_SpawnRate) {
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
                else if (randomValue < (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate + X2_SpawnRate + Heart_SpawnRate) && randomValue > (Trees_SpawnRate + Coin_SpawnRate + Bombs_SpawnRate + Ramp_SpawnRate + Shield_SpawnRate + X2_SpawnRate)) {
                    SpawnHeart();
                }
            }
        }
    }

    private void SpawnTree() {
        int randomIndex = Random.Range(1, 4);
        PoolableObject treePrefab = null;
        PoolManager poolManager = null;
        
        switch (randomIndex) {
            case 1:
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(Tree1ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
            case 2:
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(Tree2ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
            case 3:
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(Tree3ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
        }

        if (treePrefab != null) {
            treePrefab.transform.position = SpawnPoint.transform.position;
        }
    }
    private void SpawnBomb() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(BombManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = SpawnPoint.transform.position;
    }
    private void SpawnCoin() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(CoinManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = SpawnPoint.transform.position;
    }
    private void SpawnShield() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(ShieldManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = SpawnPoint.transform.position;
    }
    private void SpawnX2() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(X2ManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = SpawnPoint.transform.position;
    }
    private void SpawnHeart() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(HeartManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = SpawnPoint.transform.position;
    }
    private void SpawnRamp() {
        int randomValue = Random.Range(0, 100);
        PoolableObject rampPrefab = null;
        PoolManager poolManager = null;

        //75 spawn rate small ramp
        if (randomValue < SRamp_SpawnRate) {
            poolManager = PoolingSystem.Instance.getPoolManagerInstance(SRampManagerContainer);
            rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
        }
        //20 spawn rate medium ramp
        else if (randomValue < (SRamp_SpawnRate + MRamp_SpawnRate) && randomValue > SRamp_SpawnRate) {
            poolManager = PoolingSystem.Instance.getPoolManagerInstance(MRampManagerContainer);
            rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
        }
        //5 spawn rate big ramp
        else if (randomValue < (SRamp_SpawnRate + MRamp_SpawnRate + LRamp_SpawnRate) && randomValue > (SRamp_SpawnRate + MRamp_SpawnRate)) {
            poolManager = PoolingSystem.Instance.getPoolManagerInstance(LRampManagerContainer);
            rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
        }

        if (rampPrefab != null) {
            rampPrefab.transform.position = SpawnPoint.transform.position;
        }
    }

}
