using System.Collections;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>, ISystem {
    [Header("System Settings")]

    #region Priority
    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }
    #endregion

    #region GameEvents
    [Header("Game Events")]

    [Tooltip("Event invoked when the speed is changed")]
    [SerializeField]
    private GameEvent _SpeedChangeEvent;

    [Tooltip("Event listened when the player hit a shield")]
    [SerializeField]
    private GameEvent _ShieldHitEvent;

    [Tooltip("Event called when the player lose a shield")]
    [SerializeField]
    private GameEvent _ShieldRemovedEvent;

    [Tooltip("Event listened when the player take a heart")]
    [SerializeField]
    private GameEvent _HeartTakenEvent;

    [Tooltip("Event listened when the player take a coin")]
    [SerializeField]
    private GameEvent _CoinTakenEvent;

    [Tooltip("Event listened when the player take damage")]
    [SerializeField]
    private GameEvent _DamageTakenEvent;

    [Tooltip("Event listened when the player take a bomb")]
    [SerializeField]
    private GameEvent _BombTakenEvent;

    [Tooltip("Event listened when the player take a X2")]
    [SerializeField]
    private GameEvent _X2TakenEvent;


    [Tooltip("Event listened when the player enter a small ramp")]
    [SerializeField]
    private GameEvent _SmallRampEnterEvent;

    [Tooltip("Event listened when the player enter a medium ramp")]
    [SerializeField]
    private GameEvent _MediumRampEnterEvent;

    [Tooltip("Event listened when the player enter a big ramp")]
    [SerializeField]
    private GameEvent _BigRampEnterEvent;

    [Tooltip("Event listened when the player exit a small ramp")]
    [SerializeField]
    private GameEvent _SmallRampExitEvent;

    [Tooltip("Event listened when the player exit a medium ramp")]
    [SerializeField]
    private GameEvent _MediumRampExitEvent;


    [Tooltip("Event listened when the player exit a big ramp")]
    [SerializeField]
    private GameEvent _HardRampExitEvent;

    [Tooltip("Event to invoke when the spawn rate has to change")]
    [SerializeField]
    private GameEvent _SpawnRateChangeInvoke;

    [Tooltip("Event to invoke when the pause state has changed")]
    [SerializeField]
    private GameEvent _PauseEvent;

    [Tooltip("Event invoked when the jump is started")]
    [SerializeField]
    private GameEvent _JumpStart;

    [Tooltip("Event invoked when the jump is completed with success")]
    [SerializeField]
    private GameEvent _JumpCompleted;

    [Tooltip("Event invoked when the jump is failed")]
    [SerializeField]
    private GameEvent _JumpFailed;

    [Tooltip("Event invoked when the game is over")]
    [SerializeField]
    private GameEvent _GameOverEvent;

    #endregion

    #region variables

    [Header("Game Master Settings")]

    [SerializeField]
    [Tooltip("Starting velocity of the game")]
    private float _Velocity;

    [SerializeField]
    [Tooltip("Added incremental velocity ")]
    private float _IncVelocity;

    [SerializeField]
    [Tooltip("Starting speed")]
    private float _StartingVelocity = 3.5f;

    [SerializeField]
    [Tooltip("Point of incremental velocity")]
    private int _SpanPoint;

    [SerializeField]
    [Tooltip("Is the game paused?")]
    private bool isPaused;

    [SerializeField]
    [Tooltip("Number of hearts")]
    private int hearts;

    [SerializeField]
    [Tooltip("Player points")]
    private int points;

    private int _TMPPoints;

    [SerializeField]
    [Tooltip("If The Player has a shield")]
    private bool _hasShield;

    private int _multiplier = 1;
    private int _multiplierTimer = 0;

    [Tooltip("UI instantieated when player die")]
    [SerializeField]
    private DeathViewController _DeathViewPrefab;

    private DeathViewController _deathViewController;

    [SerializeField]
    private GameObject _SlopeEnd;

    private bool _canEasyJump = false;
    private bool _canMediumJump = false;
    private bool _canHardJump = false;

    private bool _Dead = false;
    private bool _SpeedChangeController = true;

    private int _jumpPerformed = 0;//1 = easy, 2 = medium, 3 = hard

    [SerializeField]
    private GameObject _X2MultiplierPrefab;
    private GameObject _X2Multiplier;
    #endregion

    #region Setters&Getters
    /// <summary>
    /// Sets the player's velocity to the specified value.
    /// </summary>
    /// <param name="_velocity">The new velocity value to set.</param>
    public void setVelocity(float _velocity) {
        _Velocity = _velocity;
        Debug.Log("Speed set to: " + _Velocity);
        _SpeedChangeEvent.Invoke();
    }

    /// <summary>
    /// Resets the player's velocity to the starting velocity value.
    /// </summary>
    public void SetStartingSpeed() {
        _Velocity = _StartingVelocity;
    }

    /// <summary>
    /// Retrieves the player's current velocity.
    /// </summary>
    /// <returns>The current velocity value.</returns>
    public float getVelocity() {
        return _Velocity;
    }

    /// <summary>
    /// Sets the game's pause state.
    /// </summary>
    /// <param name="paused">True to pause the game, false to resume.</param>
    public void setPause(bool paused) {
        isPaused = paused;
        _PauseEvent.Invoke();
    }

    /// <summary>
    /// Retrieves the current pause state of the game.
    /// </summary>
    /// <returns>True if the game is paused, false if it's running.</returns>
    public bool getPause() {
        return isPaused;
    }

    /// <summary>
    /// Sets the player's heart count to the specified value.
    /// </summary>
    /// <param name="_hearts">The new heart count value to set.</param>
    public void setHearts(int _hearts) {
        hearts = _hearts;
    }

    /// <summary>
    /// Retrieves the player's current heart count.
    /// </summary>
    /// <returns>The current heart count.</returns>
    public int getHearts() {
        return hearts;
    }

    /// <summary>
    /// Sets the player's points to the specified value.
    /// </summary>
    /// <param name="_points">The new points value to set.</param>
    public void setPoints(int _points) {
        points = _points;
    }

    /// <summary>
    /// Retrieves the player's current points.
    /// </summary>
    /// <returns>The current points value.</returns>
    public int getPoints() {
        return points;
    }

    /// <summary>
    /// Sets the player's death state.
    /// </summary>
    /// <param name="_dead">True if the player is dead, false otherwise.</param>
    public void setDeath(bool _dead) {
        this._Dead = _dead;
    }

    /// <summary>
    /// Retrieves the player's current death state.
    /// </summary>
    /// <returns>True if the player is dead, false otherwise.</returns>
    public bool getDeath() {
        return _Dead;
    }

    /// <summary>
    /// Retrieves the remaining time on the player's multiplier effect.
    /// </summary>
    /// <returns>The remaining time on the multiplier timer.</returns>
    public int getMultiplierTimer() {
        return _multiplierTimer;
    }

    /// <summary>
    /// Checks if the player is currently able to perform a jump.
    /// </summary>
    /// <returns>True if the player can jump, false otherwise.</returns>
    public bool canJump() {
        return (_canEasyJump || _canMediumJump || _canHardJump);
    }

    /// <summary>
    /// Retrieves whether the player can perform an easy jump.
    /// </summary>
    /// <returns>True if the player can perform an easy jump, false otherwise.</returns>
    public bool getEasyJump() {
        return _canEasyJump;
    }

    /// <summary>
    /// Retrieves whether the player can perform a medium jump.
    /// </summary>
    /// <returns>True if the player can perform a medium jump, false otherwise.</returns>
    public bool getMediumJump() {
        return _canMediumJump;
    }

    /// <summary>
    /// Retrieves whether the player can perform a hard jump.
    /// </summary>
    /// <returns>True if the player can perform a hard jump, false otherwise.</returns>
    public bool getHardJump() {
        return _canHardJump;
    }
    #endregion

    /// <summary>
    /// This method is responsible for setting up the script and subscribing it to various game events.
    /// It initializes event subscriptions, sets initial values, and notifies the system coordinator about the completion of system setup.
    /// </summary>
    public void Setup() {
        // Subscribe to various game events for event handling.
        _ShieldHitEvent.Subscribe(ShieldHit);
        _HeartTakenEvent.Subscribe(HeartTaken);
        _BombTakenEvent.Subscribe(BombTaken);
        _CoinTakenEvent.Subscribe(CoinTaken);
        _DamageTakenEvent.Subscribe(DamageTaken);
        _X2TakenEvent.Subscribe(X2Taken);
        _SmallRampEnterEvent.Subscribe(EasyRampEnter);
        _MediumRampEnterEvent.Subscribe(MediumRampEnter);
        _BigRampEnterEvent.Subscribe(HardRampEnter);
        _SmallRampExitEvent.Subscribe(EasyRampExit);
        _MediumRampExitEvent.Subscribe(MediumRampExit);
        _HardRampExitEvent.Subscribe(HardRampExit);
        _JumpStart.Subscribe(JumpStart);
        _JumpCompleted.Subscribe(JumpCompleted);
        _JumpFailed.Subscribe(JumpFailed);

        // Initialize a temporary points variable with a starting value.
        _TMPPoints = _SpanPoint;

        // Notify the system coordinator that the system setup is finished for this script component.
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    /// <summary>
    /// This method is called when the script component is disabled or removed.
    /// It unsubscribes the script from various game events to prevent event handling when the script is inactive.
    /// </summary>
    private void OnDisable() {
        // Unsubscribe from the ShieldHitEvent to stop handling shield gain events.
        _ShieldHitEvent.Unsubscribe(ShieldHit);

        // Unsubscribe from the HeartTakenEvent to stop handling heart collection events.
        _HeartTakenEvent.Unsubscribe(HeartTaken);

        // Unsubscribe from the BombTakenEvent to stop handling bomb collection events.
        _BombTakenEvent.Unsubscribe(BombTaken);

        // Unsubscribe from the CoinTakenEvent to stop handling coin collection events.
        _CoinTakenEvent.Unsubscribe(CoinTaken);

        // Unsubscribe from the DamageTakenEvent to stop handling damage events.
        _DamageTakenEvent.Unsubscribe(DamageTaken);

        // Unsubscribe from the X2TakenEvent to stop handling X2 multiplier events.
        _X2TakenEvent.Unsubscribe(X2Taken);

        // Unsubscribe from the SmallRampEnterEvent to stop handling small ramp entry events.
        _SmallRampEnterEvent.Unsubscribe(EasyRampEnter);

        // Unsubscribe from the MediumRampEnterEvent to stop handling medium ramp entry events.
        _MediumRampEnterEvent.Unsubscribe(MediumRampEnter);

        // Unsubscribe from the BigRampEnterEvent to stop handling big ramp entry events.
        _BigRampEnterEvent.Unsubscribe(HardRampEnter);

        // Unsubscribe from the SmallRampExitEvent to stop handling small ramp exit events.
        _SmallRampExitEvent.Unsubscribe(EasyRampExit);

        // Unsubscribe from the MediumRampExitEvent to stop handling medium ramp exit events.
        _MediumRampExitEvent.Unsubscribe(MediumRampExit);

        // Unsubscribe from the HardRampExitEvent to stop handling hard ramp exit events.
        _HardRampExitEvent.Unsubscribe(HardRampExit);

        // Unsubscribe from the JumpStart event to stop handling jump initiation events.
        _JumpStart.Unsubscribe(JumpStart);

        // Unsubscribe from the JumpCompleted event to stop handling jump completion events.
        _JumpCompleted.Unsubscribe(JumpCompleted);

        // Unsubscribe from the JumpFailed event to stop handling jump failure events.
        _JumpFailed.Unsubscribe(JumpFailed);
    }

    /// <summary>
    /// This method is called when the player gains a shield, typically triggered by a game event.
    /// It handles the effects of gaining a shield, including setting the '_hasShield' flag to true.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the shield gain event.</param>
    void ShieldHit(GameEvent evt) {
        // Log a message indicating that a shield was gained.
        Debug.Log("Shield gained");

        // Check if the player does not already have a shield.
        if (!_hasShield) {
            // If the player does not have a shield, set the '_hasShield' flag to true.
            _hasShield = true;
        }
    }

    /// <summary>
    /// This method is called when the player collects a heart, typically triggered by a game event.
    /// It handles the effects of collecting a heart, including increasing the player's heart count if it's less than the maximum.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the heart collection.</param>
    void HeartTaken(GameEvent evt) {
        // Log a message indicating that a heart was collected.
        Debug.Log("Heart taken");

        // Check if the player's current heart count is less than the maximum (3).
        if (hearts < 3) {
            // If so, increase the player's heart count by one.
            hearts++;
        }
    }

    /// <summary>
    /// This method is called when the player collects a coin, typically triggered by a game event.
    /// It handles the effects of collecting a coin, including point increment based on the player's multiplier.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the coin collection.</param>
    void CoinTaken(GameEvent evt) {
        // Log a message indicating that a coin was collected.
        Debug.Log("Coin taken");

        // Increase the player's points by 100, multiplied by the current multiplier.
        setPoints(points + (100 * _multiplier));
    }

    /// <summary>
    /// This method is called when the player takes damage, typically triggered by a game event.
    /// It handles the effects of taking damage, including shield removal, heart reduction, and triggering a game over if hearts reach zero.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the damage event.</param>
    void DamageTaken(GameEvent evt) {
        // Log a message indicating that damage was taken.
        Debug.Log("Damage taken");

        // Check if the player has an active shield.
        if (_hasShield) {
            // If a shield is active, remove it and trigger a shield removed event.
            _hasShield = false;
            _ShieldRemovedEvent.Invoke();
        } else {
            // If the player does not have a shield, reduce the number of hearts by one.
            setHearts(hearts - 1);

            // Check if the player's hearts have reached zero, indicating a game over.
            if (hearts <= 0) {
                // Trigger the game over sequence.
                GameOver();
            }
        }
    }

    /// <summary>
    /// This method is called when the player collects a bomb, typically triggered by a game event.
    /// It handles the effects of collecting a bomb, including shield removal, heart reduction, and triggering a game over if hearts reach zero.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the bomb collection.</param>
    void BombTaken(GameEvent evt) {
        // Log a message indicating that a bomb was collected.
        Debug.Log("Bomb taken");

        // Check if the player has an active shield.
        if (_hasShield) {
            // If a shield is active, remove it and trigger a shield removed event.
            _hasShield = false;
            _ShieldRemovedEvent.Invoke();
        } else {
            // If the player does not have a shield, reduce the number of hearts by one.
            setHearts(hearts - 1);

            // Check if the player's hearts have reached zero, indicating a game over.
            if (hearts <= 0) {
                // Trigger the game over sequence.
                GameOver();
            }
        }
    }

    /// <summary>
    /// This method is called when the player collects a "X2" multiplier, typically triggered by a game event.
    /// It handles the activation of the multiplier effect, timer updates, and instantiation of the multiplier visual element.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the "X2" multiplier collection.</param>
    void X2Taken(GameEvent evt) {
        // Check if the X2 multiplier visual element is null; if so, instantiate it.
        if (_X2Multiplier == null)
            _X2Multiplier = Instantiate(_X2MultiplierPrefab);

        // Log a message indicating that the X2 multiplier was collected.
        Debug.Log("X2 taken");

        // Check if the player's multiplier is currently at 1.
        if (_multiplier == 1) {
            // Increase the player's multiplier to 2.
            _multiplier++;

            // Set the multiplier timer to 10 seconds.
            _multiplierTimer = 10;

            // Start the coroutine to reset the multiplier after the timer expires.
            StartCoroutine(ResetMultiplier());
        } else {
            // If the player's multiplier is already greater than 1, add 10 seconds to the multiplier timer.
            _multiplierTimer += 10;
        }
    }

    /// <summary>
    /// This coroutine resets the player's multiplier over a period of time.
    /// </summary>
    private IEnumerator ResetMultiplier() {
        // Continue the loop as long as the multiplier timer is greater than zero.
        while (_multiplierTimer > 0) {
            // Wait for one second.
            yield return new WaitForSeconds(1);

            // Decrease the multiplier timer by one second.
            _multiplierTimer--;
        }

        // Reset the player's multiplier to 1.
        _multiplier = 1;

        // Destroy the X2 multiplier GameObject.
        Destroy(_X2Multiplier);
    }

    #region Ramp Events
    public void EasyRampEnter(GameEvent evt) {
        _canEasyJump = true;
    }
    public void EasyRampExit(GameEvent evt) {
        _canEasyJump = false;
    }

    public void MediumRampEnter(GameEvent evt) {
        _canMediumJump = true;
    }
    public void MediumRampExit(GameEvent evt) {
        _canMediumJump = false;
    }

    public void HardRampEnter(GameEvent evt) {
        _canHardJump = true;
    }
    public void HardRampExit(GameEvent evt) {
        _canHardJump = false;
    }
    #endregion

    /// <summary>
    /// This method is called when the game is over. It handles game over events, UI display, and object pooling.
    /// </summary>
    public void GameOver() {
        // Invoke the game over event.
        _GameOverEvent.Invoke();

        // Log a game over message to the console.
        Debug.Log("Game Over");

        // If the death view controller is already instantiated, return early.
        if (_deathViewController) return;

        // Instantiate the death view prefab to display game over UI.
        _deathViewController = Instantiate(_DeathViewPrefab);

        // Set the player's state to dead.
        _Dead = true;

        // Find all poolable objects in the scene and reset their positions to the slope end.
        PoolableObject[] allObjectsInScene = FindObjectsOfType<PoolableObject>();
        foreach (PoolableObject obj in allObjectsInScene) {
            if (obj.isActiveAndEnabled) {
                obj.transform.position = _SlopeEnd.transform.position;
            }
        }
    }

    /// <summary>
    /// This method is called once per frame and is responsible for handling point updates and speed adjustments.
    /// </summary>
    private void Update() {
        // Check if the player's points have exceeded a certain threshold, and if so, update temporary points and reset speed control.
        if (points > _TMPPoints) {
            _TMPPoints = _TMPPoints + _SpanPoint;
            _SpeedChangeController = false;
        }

        // Check if the player is not dead, speed control is not active, and points are not zero.
        if (!_Dead && !_SpeedChangeController && points != 0) {
            // Invoke a spawn rate change event, increase velocity, and activate speed control.
            _SpawnRateChangeInvoke.Invoke();
            setVelocity(_Velocity + _IncVelocity);
            _SpeedChangeController = true;
        }
    }

    /// <summary>
    /// This method is called when a jump action is initiated, typically triggered by a game event.
    /// It determines the type of jump to perform based on the available jump options and sets the '_jumpPerformed' value accordingly.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the jump initiation.</param>
    private void JumpStart(GameEvent evt) {
        if (_canEasyJump) {
            // Perform an easy jump
            _jumpPerformed = 1;
        } else if (_canMediumJump) {
            // Perform a medium jump
            _jumpPerformed = 2;
        } else if (_canHardJump) {
            // Perform a hard jump
            _jumpPerformed = 3;
        }
    }

    /// <summary>
    /// This method is called when a jump action is successfully completed, typically triggered by a game event.
    /// It calculates the jump points based on the number of jumps performed, resets the '_jumpPerformed' counter,
    /// and updates the player's points based on the calculated jump points and a multiplier.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the jump completion.</param>
    private void JumpCompleted(GameEvent evt) {
        float jumpPoint = 0f;

        // Calculate jump points based on the number of jumps performed
        if (_jumpPerformed == 1) {
            jumpPoint = 300f;
        } else if (_jumpPerformed == 2) {
            jumpPoint = 600f;
        } else if (_jumpPerformed == 3) {
            jumpPoint = 1000f;
        }

        // Reset the jumpPerformed counter to zero
        _jumpPerformed = 0;

        // Update the player's points based on the calculated jump points and a multiplier
        setPoints(points + ((int)jumpPoint * _multiplier));
    }

    /// <summary>
    /// This method is called when a jump action fails, typically triggered by a game event.
    /// It resets the '_jumpPerformed' counter to zero and invokes a damage taken event.
    /// </summary>
    /// <param name="evt">The GameEvent that triggered the jump failure.</param>
    private void JumpFailed(GameEvent evt) {
        // Reset the jumpPerformed counter to zero
        _jumpPerformed = 0;

        // Invoke a damage taken event
        _DamageTakenEvent.Invoke();
    }
}
