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

    [Tooltip("Event listened when the player take a heart")]
    [SerializeField]
    private GameEvent _HeartTakenEvent;

    [Tooltip("Event listened when the player take a coin")]
    [SerializeField]
    private GameEvent _CoinTakenEvent;

    [Tooltip("Event listened when the player take damage")]
    [SerializeField]
    private GameEvent _DamageTakenEvent;

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

    #endregion

    #region variables

    [Header("Game Master Settings")]

    [SerializeField]
    [Tooltip("Starting velocity of the game")]
    private int velocity;
    [SerializeField]
    [Tooltip("Second velocity of the game")]
    private int velocityLevelTwo;
    [SerializeField]
    [Tooltip("Third velocity of the game")]
    private int velocityLevelThree;

    [SerializeField]
    [Tooltip("Is the game paused?")]
    private bool isPaused;

    [SerializeField]
    [Tooltip("Number of hearts")]
    private int hearts;

    [SerializeField]
    [Tooltip("Player points")]
    private int points;

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

    private bool _levelOneControl = false;
    private bool _levelTwoControl = false;
    private bool _dead = false;
    #endregion

    #region Setters&Getters
    public void setVelocity(int _velocity) {
        velocity = _velocity;
        _SpeedChangeEvent.Invoke();
    }
    public int getVelocity() {
        return velocity;
    }

    public void setPause(bool paused) {
        isPaused = paused;
    }
    public bool getPause() {
        return isPaused;
    }

    public void setHearts(int _hearts) {
        hearts = _hearts;
    }
    public int getHearts() {
        return hearts;
    }

    public void setPoints(int _points) {
        points = _points;
    }
    public int getPoints() {
        return points;
    }

    public void setDeath(bool _dead) {
        this._dead = _dead;
    }
    public bool getDeath() {
        return _dead;
    }
    #endregion

    public void Setup() {
        //subscibe to the event
        _ShieldHitEvent.Subscribe(ShieldHit);
        _HeartTakenEvent.Subscribe(HeartTaken);
        _CoinTakenEvent.Subscribe(CoinTaken);
        _DamageTakenEvent.Subscribe(DamageTaken);
        _X2TakenEvent.Subscribe(X2Taken);
        _SmallRampEnterEvent.Subscribe(EasyRampEnter);
        _MediumRampEnterEvent.Subscribe(MediumRampEnter);
        _BigRampEnterEvent.Subscribe(HardRampEnter);
        _SmallRampExitEvent.Subscribe(EasyRampExit);
        _MediumRampExitEvent.Subscribe(MediumRampExit);
        _HardRampExitEvent.Subscribe(HardRampExit);

        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    private void OnDisable() {
        _ShieldHitEvent.Unsubscribe(ShieldHit);
        _HeartTakenEvent.Unsubscribe(HeartTaken);
        _CoinTakenEvent.Unsubscribe(CoinTaken);
        _DamageTakenEvent.Unsubscribe(DamageTaken);
        _X2TakenEvent.Unsubscribe(X2Taken);
        _SmallRampEnterEvent.Unsubscribe(EasyRampEnter);
        _MediumRampEnterEvent.Unsubscribe(MediumRampEnter);
        _BigRampEnterEvent.Unsubscribe(HardRampEnter);
        _SmallRampExitEvent.Unsubscribe(EasyRampExit);
        _MediumRampExitEvent.Unsubscribe(MediumRampExit);
        _HardRampExitEvent.Unsubscribe(HardRampExit);
    }

    //used for testing
    // private void OnEnable()
    // {
    //     _ShieldHitEvent.Subscribe(ShieldHit);
    //     _HeartTakenEvent.Subscribe(HeartTaken);
    //     _CoinTakenEvent.Subscribe(CoinTaken);
    //     _DamageTakenEvent.Subscribe(DamageTaken);
    //     _X2TakenEvent.Subscribe(X2Taken);
    // }


    //Event handler
    void ShieldHit(GameEvent evt) {
        Debug.Log("Shield gained");
        if (!_hasShield) {
            _hasShield = true;
        }
    }

    void HeartTaken(GameEvent evt) {
        Debug.Log("Heart taken");

        if (hearts < 3) {
            hearts++;
        }
        //hearts++;
    }

    void CoinTaken(GameEvent evt) {
        Debug.Log("Coin taken");
        setPoints(points + (100 * _multiplier));
    }

    void DamageTaken(GameEvent evt) {
        Debug.Log("Damage taken");
        if (_hasShield) {
            _hasShield = false;
        } else {
            setHearts(hearts - 1);
            if (hearts <= 0) {
                //game over
                GameOver();
            }
        }
    }

    void X2Taken(GameEvent evt) {
        Debug.Log("X2 taken");
        if (_multiplier == 1) {
            _multiplier++;
            //Debug.Log("Multiplier taken ");
            _multiplierTimer = 10;
            StartCoroutine(ResetMultiplier());
        }
    }

    private IEnumerator ResetMultiplier() {
        while (_multiplierTimer > 0) {
            yield return new WaitForSeconds(1);
            _multiplierTimer--;
            //Debug.Log("Multiplier timer: " + _multiplierTimer);
        }
        //yield return new WaitForSeconds(10);
        _multiplier = 1;

        //Debug.Log("Multiplier reset");
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


    //Gameover
    public void GameOver() {
        Debug.Log("Game Over");
        if (_deathViewController) return;
        _deathViewController = Instantiate(_DeathViewPrefab);
        isPaused = true;
        _dead = true;
        this._levelOneControl = false;
        this._levelTwoControl = false;

        PoolableObject[] allObjectsInScene = FindObjectsOfType<PoolableObject>();
        foreach (PoolableObject obj in allObjectsInScene) {
            if (obj.isActiveAndEnabled) {
                obj.transform.position = _SlopeEnd.transform.position;
            }
        }
    }

    //Velocity Test
    [ContextMenu("InvokeEvent")]
    public void InvokeEvent() {
        setVelocity(10);
        
    }
    
    private void Update() {
        if(points >= 1000 && !_levelOneControl && !_dead) {
            _SpawnRateChangeInvoke.Invoke();
            velocity = velocityLevelTwo;
            _SpeedChangeEvent.Invoke();
            _levelOneControl = true;
        }
        if (points >= 2000 && !_levelTwoControl && !_dead) {
            _SpawnRateChangeInvoke.Invoke();
            velocity = velocityLevelThree;
            _SpeedChangeEvent.Invoke();
            _levelTwoControl = true;
        }
    }
}
