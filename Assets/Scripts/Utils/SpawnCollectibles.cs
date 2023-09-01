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
    /// <summary>
    /// Sets the spawn rate of avalanches to the specified value.
    /// </summary>
    /// <param name="value">The new spawn rate value to set.</param>
    public void setSpawnRate(float value) {
        this._SpawnRate = value;
    }

    /// <summary>
    /// Retrieves the current spawn rate of avalanches.
    /// </summary>
    /// <returns>The current spawn rate value.</returns>
    public float getSpawnRate() {
        return this._SpawnRate;
    }
    #endregion

    /// <summary>
    /// This method is called when the MonoBehaviour is initialized.
    /// It subscribes the script to various game events, sets the player's death status to false,
    /// and starts a coroutine to spawn collectibles.
    /// </summary>
    private void Start() {
        // Subscribe to SpawnRateChangeEvent to handle changes in the spawn rate.
        _SpawnRateChangeEvent.Subscribe(SpawnRateChange);

        // Subscribe to JumpStartedEvent to handle jump initiation events.
        _JumpStartedEvent.Subscribe(JumpStart);

        // Subscribe to JumpCompletedEvent to handle successful jump completion events.
        _JumpCompletedEvent.Subscribe(JumpCompleted);

        // Subscribe to JumpFailedEvent to handle jump failure events.
        _JumpFailedEvent.Subscribe(JumpFailed);

        // Set the player's death status to false.
        GameMaster.Instance.setDeath(false);

        // Start a coroutine to spawn collectibles.
        StartCoroutine(SpawnCollectiblesCoroutine());
    }

    /// <summary>
    /// This method is called when the MonoBehaviour is disabled or removed.
    /// It unsubscribes the script from various game events to prevent event handling when the script is inactive.
    /// </summary>
    private void OnDisable() {
        // Unsubscribe from SpawnRateChangeEvent to stop handling spawn rate change events.
        _SpawnRateChangeEvent.Unsubscribe(SpawnRateChange);

        // Unsubscribe from JumpStartedEvent to stop handling jump initiation events.
        _JumpStartedEvent.Unsubscribe(JumpStart);

        // Unsubscribe from JumpCompletedEvent to stop handling successful jump completion events.
        _JumpCompletedEvent.Unsubscribe(JumpCompleted);

        // Unsubscribe from JumpFailedEvent to stop handling jump failure events.
        _JumpFailedEvent.Unsubscribe(JumpFailed);
    }

    /// <summary>
    /// A coroutine responsible for spawning various collectible items at random intervals.
    /// </summary>
    private IEnumerator SpawnCollectiblesCoroutine() {
        while (true) {
            // Randomly adjust the spawn point's horizontal position within a range.
            _SpawnPoint.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), _SpawnPoint.transform.position.y, _SpawnPoint.transform.position.z);

            yield return new WaitForSeconds(_SpawnRate);
            Debug.Log("Waited " + _SpawnRate + " seconds");

            if (!GameMaster.Instance.getPause()) {
                // Generate a random value to determine which collectible item to spawn.
                int randomValue = Random.Range(0, 100);

                if (randomValue < _Trees_SpawnRate) {
                    SpawnTree();
                } else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate) && randomValue > _Trees_SpawnRate) {
                    SpawnCoin();
                } else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate)) {
                    SpawnBomb();
                } else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate)) {
                    SpawnRamp();
                } else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate)) {
                    SpawnShield();
                } else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate + _X2_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate)) {
                    SpawnX2();
                } else if (randomValue < (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate + _X2_SpawnRate + _Heart_SpawnRate) && randomValue > (_Trees_SpawnRate + _Coin_SpawnRate + _Bombs_SpawnRate + _Ramp_SpawnRate + _Shield_SpawnRate + _X2_SpawnRate)) {
                    SpawnHeart();
                }
            }
        }
    }

    /// <summary>
    /// Spawns a tree collectible at a random location using object pooling.
    /// </summary>
    private void SpawnTree() {
        int randomIndex = Random.Range(1, 4);
        PoolableObject treePrefab = null;
        PoolManager poolManager = null;

        switch (randomIndex) {
            case 1:
                // Get the pool manager for the first tree type.
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_Tree1ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
            case 2:
                // Get the pool manager for the second tree type.
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_Tree2ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
            case 3:
                // Get the pool manager for the third tree type.
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_Tree3ManagerContainer);
                treePrefab = poolManager.GetPoolableObject<PoolableObject>();
                break;
        }

        if (treePrefab != null) {
            // Set the tree's position to the spawn point.
            treePrefab.transform.position = _SpawnPoint.transform.position;
        }
    }

    /// <summary>
    /// Spawns a bomb collectible at the specified spawn point using object pooling.
    /// </summary>
    private void SpawnBomb() {
        // Get the pool manager for bomb collectibles.
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_BombManagerContainer);

        // Get a bomb collectible object from the pool.
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();

        // Set the bomb's position to the spawn point.
        prefab.transform.position = _SpawnPoint.transform.position;
    }

    /// <summary>
    /// Spawns a coin collectible at the specified spawn point using object pooling.
    /// </summary>
    private void SpawnCoin() {
        // Get the pool manager for coin collectibles.
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_CoinManagerContainer);

        // Get a coin collectible object from the pool.
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();

        // Set the coin's position to the spawn point.
        prefab.transform.position = _SpawnPoint.transform.position;
    }

    /// <summary>
    /// Spawns a shield collectible at the specified spawn point using object pooling.
    /// </summary>
    private void SpawnShield() {
        // Get the pool manager for shield collectibles.
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_ShieldManagerContainer);

        // Get a shield collectible object from the pool.
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();

        // Set the shield's position to the spawn point.
        prefab.transform.position = _SpawnPoint.transform.position;
    }

    /// <summary>
    /// Spawns an "X2" multiplier collectible at the specified spawn point using object pooling.
    /// </summary>
    private void SpawnX2() {
        // Get the pool manager for "X2" multiplier collectibles.
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_X2ManagerContainer);

        // Get an "X2" multiplier collectible object from the pool.
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();

        // Set the "X2" multiplier's position to the spawn point.
        prefab.transform.position = _SpawnPoint.transform.position;
    }

    /// <summary>
    /// Spawns a heart collectible at the specified spawn point using object pooling.
    /// </summary>
    private void SpawnHeart() {
        // Get the pool manager for heart collectibles.
        PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_HeartManagerContainer);

        // Get a heart collectible object from the pool.
        PoolableObject prefab = poolManager.GetPoolableObject<PoolableObject>();

        // Set the heart's position to the spawn point.
        prefab.transform.position = _SpawnPoint.transform.position;
    }

    /// <summary>
    /// Spawns a ramp collectible at the specified spawn point using object pooling.
    /// </summary>
    private void SpawnRamp() {
        // Find any existing object with the "Ramp" tag.
        GameObject objectWithTag = GameObject.FindWithTag("Ramp");

        if (objectWithTag == null) {
            int randomValue = Random.Range(0, 100);
            PoolableObject rampPrefab = null;
            PoolManager poolManager = null;

            if (randomValue < _SRamp_SpawnRate) {
                // Get the pool manager for small ramp collectibles.
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_SRampManagerContainer);
                rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
            } else if (randomValue < (_SRamp_SpawnRate + _MRamp_SpawnRate) && randomValue > _SRamp_SpawnRate) {
                // Get the pool manager for medium ramp collectibles.
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_MRampManagerContainer);
                rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
            } else if (randomValue < (_SRamp_SpawnRate + _MRamp_SpawnRate + _LRamp_SpawnRate) && randomValue > (_SRamp_SpawnRate + _MRamp_SpawnRate)) {
                // Get the pool manager for large ramp collectibles.
                poolManager = PoolingSystem.Instance.getPoolManagerInstance(_LRampManagerContainer);
                rampPrefab = poolManager.GetPoolableObject<PoolableObject>();
            }

            if (rampPrefab != null) {
                // Set the spawn point's horizontal position to the center.
                _SpawnPoint.transform.position = new Vector3(0, _SpawnPoint.transform.position.y, _SpawnPoint.transform.position.z);

                // Set the ramp's position to the spawn point.
                rampPrefab.transform.position = _SpawnPoint.transform.position;
            }
        } else {
            Debug.Log("Ramp already spawned");
        }
    }

    /// <summary>
    /// Handles a change in the spawn rate, decreasing it up to a minimum threshold.
    /// </summary>
    /// <param name="evt">The GameEvent triggering the spawn rate change.</param>
    private void SpawnRateChange(GameEvent evt) {
        // Decrease the spawn rate as long as it is above the minimum threshold.
        if (this._SpawnRate >= _SpawnRateStopDecrease) {
            this._SpawnRate -= _SpawnRateDecrease;
        } else {
            // Set the spawn rate to the minimum threshold when it cannot decrease further.
            this._SpawnRate = _SpawnRateStopDecrease;
        }
    }

    /// <summary>
    /// Initiates a jump event, temporarily slowing down the spawn rate for collectibles.
    /// </summary>
    /// <param name="evt">The GameEvent triggering the jump event.</param>
    private void JumpStart(GameEvent evt) {
        // Store the current spawn rate before slowing it down.
        oldSpawnRate = _SpawnRate;

        // Set the spawn rate to a slowed value during the jump event.
        _SpawnRate = _SlowedSpawnRate;
    }

    /// <summary>
    /// Reset the spawn rate to its original value after the jump event has completed.
    /// </summary>
    /// <param name="evt">The GameEvent triggering the jump event.</param>
    private void JumpCompleted(GameEvent evt) {
        // Reset the spawn rate to its original value.
        _SpawnRate = oldSpawnRate;
    }

    /// <summary>
    /// Reset the spawn rate to its original value after the jump event has failed.
    /// </summary>
    /// <param name="evt">The GameEvent triggering the jump event.</param>
    private void JumpFailed(GameEvent evt) {
        // Reset the spawn rate to its original value.
        _SpawnRate = oldSpawnRate;
    }
}