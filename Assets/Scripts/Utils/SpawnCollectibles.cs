using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the spawning of various collectibles in the game.
/// </summary>
public class SpawnCollectibles : MonoBehaviour {
    #region variables
    [Header("Spawn Settings")]

    [SerializeField]
    [Tooltip("Seconds of spawn rate")]
    private float _SpawnRate;
    [SerializeField]
    [Tooltip("Seconds of spawn rate decrease")]
    private float _SpawnRateDecrease;
    [SerializeField]
    [Tooltip("Minimum seconds of spawn rate")]
    private float _SpawnRateStopDecrease;
    [SerializeField]
    [Tooltip("Slowed seconds of spawn rate")]
    private float _SlowedSpawnRate;

    private float oldSpawnRate;

    [SerializeField]
    [Tooltip("Trees spawn rate")]
    private int _Trees_SpawnRate;
    [SerializeField]
    private IdContainer _Tree1ManagerContainer;
    [SerializeField]
    private IdContainer _Tree2ManagerContainer;
    [SerializeField]
    private IdContainer _Tree3ManagerContainer;

    [SerializeField]
    [Tooltip("Bombs spawn rate")]
    private int _Bombs_SpawnRate;
    [SerializeField]
    private IdContainer _BombManagerContainer;

    [SerializeField]
    [Tooltip("Coin spawn rate")]
    private int _Coin_SpawnRate;
    [SerializeField]
    private IdContainer _CoinManagerContainer;

    [SerializeField]
    [Tooltip("Shield spawn rate")]
    private int _Shield_SpawnRate;
    [SerializeField]
    private IdContainer _ShieldManagerContainer;

    [SerializeField]
    [Tooltip("X2 spawn rate")]
    private int _X2_SpawnRate;
    [SerializeField]
    private IdContainer _X2ManagerContainer;

    [SerializeField]
    [Tooltip("Heart spawn rate")]
    private int _Heart_SpawnRate;
    [SerializeField]
    private IdContainer _HeartManagerContainer;

    [SerializeField]
    [Tooltip("Ramp spawn rate")]
    private int _Ramp_SpawnRate;
    [SerializeField]
    [Tooltip("Small ramp spawn rate")]
    private int _SRamp_SpawnRate;
    [SerializeField]
    [Tooltip("Medium ramp spawn rate")]
    private int _MRamp_SpawnRate;
    [SerializeField]
    [Tooltip("Big ramp spawn rate")]
    private int _LRamp_SpawnRate;
    [SerializeField]
    private IdContainer _SRampManagerContainer;
    [SerializeField]
    private IdContainer _MRampManagerContainer;
    [SerializeField]
    private IdContainer _LRampManagerContainer;

    [SerializeField]
    private GameObject _SpawnPoint;
    #endregion

    #region GameEvents
    [Tooltip("Event invoked when the spawn rate is changed")]
    [SerializeField]
    private GameEvent _SpawnRateChangeEvent;

    [Tooltip("Event listened when a jump is performed")]
    [SerializeField]
    private GameEvent _JumpStartedEvent;

    [Tooltip("Event listened when a jump is completed")]
    [SerializeField]
    private GameEvent _JumpCompletedEvent;

    [Tooltip("Event listened when a jump is failed")]
    [SerializeField]
    private GameEvent _JumpFailedEvent;
    #endregion

    #region Setters&Getters
    public void setSpawRate(float value) {
        this._SpawnRate = value;
    }
    public float getSpawnRate() {
        return this._SpawnRate;
    }
    #endregion

    private void Start() {
        _SpawnRateChangeEvent.Subscribe(SpawnRateChange);
        _JumpStartedEvent.Subscribe(JumpStart);
        _JumpCompletedEvent.Subscribe(JumpCompleted);
        _JumpFailedEvent.Subscribe(JumpFailed);

        GameMaster.Instance.setDeath(false);
        StartCoroutine(SpawnCollectiblesCoroutine());
    }

    private void OnDisable() {
        _SpawnRateChangeEvent.Unsubscribe(SpawnRateChange);
        _JumpStartedEvent.Unsubscribe(JumpStart);
        _JumpCompletedEvent.Unsubscribe(JumpCompleted);
        _JumpFailedEvent.Unsubscribe(JumpFailed);
    }

    private IEnumerator SpawnCollectiblesCoroutine() {
        while (true) {
            // Move the spawn point.
            _SpawnPoint.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), _SpawnPoint.transform.position.y, _SpawnPoint.transform.position.z);

            // Wait for the specified spawn rate.
            yield return new WaitForSeconds(_SpawnRate);
            Debug.Log("Waited " + _SpawnRate + " seconds");

            if (!GameMaster.Instance.getPause()) {
                int randomValue = Random.Range(0, 100);
                // Determine the type of collectible to spawn based on the randomValue.
                // Call the appropriate spawning method.

                //60 spawn rate trees
                if (randomValue < _Trees_SpawnRate) {
                    SpawnTree();
                }
                //20 spawn rate coins
                else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate) && randomValue > _Trees_SpawnRate) {
                    SpawnCoin();
                }
                //6 spawn rate bombs
                else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate)) {
                    SpawnBomb();
                }
                //6 spawn rate ramps
                else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate)) {
                    SpawnRamp();
                }
                //4 spawn rate shield
                else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate)) {
                    SpawnShield();
                }
                //2 spawn rate X2
                else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate + _X2_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate)) {
                    SpawnX2();
                }
                //2 spawn rate Heart
                else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate + _X2_SpawnRate + _Heart_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate + _X2_SpawnRate)) {
                    SpawnHeart();
                }
            }
        }
    }

    // Methods for spawning different types of collectibles, similar to the example above.
    private void SpawnTree() {
        int randomIndex = Random.Range(1, 4);
        PoolableObject treePrefab = null;
        PoolManager poolManager = null;

        switch (randomIndex) {
            case 1:
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_Tree1ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
            case 2:
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_Tree2ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
            case 3:
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_Tree3ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
        }

        if (treePrefab != null) {
            treePrefab.transform.position = _SpawnPoint.transform.position;
        }
    }
    private void SpawnBomb() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_BombManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = _SpawnPoint.transform.position;
    }
    private void SpawnCoin() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_CoinManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = _SpawnPoint.transform.position;
    }
    private void SpawnShield() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_ShieldManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = _SpawnPoint.transform.position;
    }
    [ContextMenu("SpawnX2")]
    private void SpawnX2() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_X2ManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = _SpawnPoint.transform.position;
    }
    private void SpawnHeart() {
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_HeartManagerContainer);
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();
        prefab.transform.position = _SpawnPoint.transform.position;
    }
    private void SpawnRamp() {

        GameObject objectWithTag = GameObject.FindWithTag("Ramp");

        if (objectWithTag == null) {
            int randomValue = Random.Range(0, 100);
            PoolableObject rampPrefab = null;
            PoolManager poolManager = null;

            //75 spawn rate small ramp
            if (randomValue < _SRamp_SpawnRate) {
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_SRampManagerContainer);
                rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
            }
            //20 spawn rate medium ramp
            else if (randomValue < (_SRamp_SpawnRate + _MRamp_SpawnRate) && randomValue > _SRamp_SpawnRate) {
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_MRampManagerContainer);
                rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
            }
            //5 spawn rate big ramp
            else if (randomValue < (_SRamp_SpawnRate + _MRamp_SpawnRate + _LRamp_SpawnRate) && randomValue > (_SRamp_SpawnRate + _MRamp_SpawnRate)) {
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_LRampManagerContainer);
                rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
            }

            if (rampPrefab != null) {
                _SpawnPoint.transform.position = new Vector3(0, _SpawnPoint.transform.position.y, _SpawnPoint.transform.position.z);
                rampPrefab.transform.position = _SpawnPoint.transform.position;
            }
        } else
            Debug.Log("Ramp already spawned");
    }

    private void SpawnRateChange(GameEvent evt) {
        if (this._SpawnRate >= _SpawnRateStopDecrease) {
            this._SpawnRate -= _SpawnRateDecrease;
        } else {
            this._SpawnRate = _SpawnRateStopDecrease;
        }
    }

    private void JumpStart (GameEvent evt) {
       oldSpawnRate = _SpawnRate;
       _SpawnRate = _SlowedSpawnRate;
    }

    private void JumpCompleted(GameEvent evt) {
        _SpawnRate = oldSpawnRate;
    }

    private void JumpFailed(GameEvent evt) {
        _SpawnRate = oldSpawnRate;
    }
}