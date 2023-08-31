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
    public void setVelocity(float _velocity) {
        _Velocity = _velocity;
        Debug.Log("Sped setted to: " + _Velocity);
        _SpeedChangeEvent.Invoke();
    }

    public void SetStartingSpeed() {
        _Velocity = _StartingVelocity;
    }
    public float getVelocity() {
        return _Velocity;
    }

    public void setPause(bool paused) {
        isPaused = paused;
        _PauseEvent.Invoke();
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
        this._Dead = _dead;
    }
    public bool getDeath() {
        return _Dead;
    }

    public int getMultiplierTimer() {
        return _multiplierTimer;
    }

    public bool canJump() {
        if (_canEasyJump || _canMediumJump || _canHardJump) {
            return true;
        }
        return false;
    }

    public bool getEasyJump() {
        return _canEasyJump;
    }

    public bool getMediumJump() {
        return _canMediumJump;
    }

    public bool getHardJump() {
        return _canHardJump;
    }
    #endregion

    public void Setup() {
        //subscibe to the event
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

        _TMPPoints = _SpanPoint;

        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    private void OnDisable() {
        _ShieldHitEvent.Unsubscribe(ShieldHit);
        _HeartTakenEvent.Unsubscribe(HeartTaken);
        _BombTakenEvent.Unsubscribe(BombTaken);
        _CoinTakenEvent.Unsubscribe(CoinTaken);
        _DamageTakenEvent.Unsubscribe(DamageTaken);
        _X2TakenEvent.Unsubscribe(X2Taken);
        _SmallRampEnterEvent.Unsubscribe(EasyRampEnter);
        _MediumRampEnterEvent.Unsubscribe(MediumRampEnter);
        _BigRampEnterEvent.Unsubscribe(HardRampEnter);
        _SmallRampExitEvent.Unsubscribe(EasyRampExit);
        _MediumRampExitEvent.Unsubscribe(MediumRampExit);
        _HardRampExitEvent.Unsubscribe(HardRampExit);
        _JumpStart.Unsubscribe(JumpStart);
        _JumpCompleted.Unsubscribe(JumpCompleted);
        _JumpFailed.Unsubscribe(JumpFailed);
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
            _ShieldRemovedEvent.Invoke();
        } else {
            setHearts(hearts - 1);
            if (hearts <= 0) {
                //game over
                GameOver();
            }
        }
    }

    void BombTaken(GameEvent evt) {
        Debug.Log("Bomb taken");
        if (_hasShield) {
            _hasShield = false;
            _ShieldRemovedEvent.Invoke();
        } else {
            setHearts(hearts - 1);
            if (hearts <= 0) {
                //game over
                GameOver();
            }
        }
    }

    void X2Taken(GameEvent evt) {
        if (_X2Multiplier == null)
            _X2Multiplier = Instantiate(_X2MultiplierPrefab);
        Debug.Log("X2 taken");
        if (_multiplier == 1) {
            _multiplier++;
            //Debug.Log("Multiplier taken ");
            _multiplierTimer = 10;
            StartCoroutine(ResetMultiplier());
        } else {
            _multiplierTimer += 10;
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
        Destroy(_X2Multiplier);
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
        _GameOverEvent.Invoke();
        Debug.Log("Game Over");
        if (_deathViewController) return;
        _deathViewController = Instantiate(_DeathViewPrefab);
        isPaused = true;
        _Dead = true;

        PoolableObject[] allObjectsInScene = FindObjectsOfType<PoolableObject>();
        foreach (PoolableObject obj in allObjectsInScene) {
            if (obj.isActiveAndEnabled) {
                obj.transform.position = _SlopeEnd.transform.position;
            }
        }
        isPaused = false;
    }

    private void Update() {
        if(points > _TMPPoints) {
            _TMPPoints = _TMPPoints + _SpanPoint;
            _SpeedChangeController = false;
        }
        if (!_Dead && !_SpeedChangeController && points != 0) {
            _SpawnRateChangeInvoke.Invoke();
            setVelocity(_Velocity + _IncVelocity);
            _SpeedChangeController = true;
        }
    }

    private void JumpStart(GameEvent evt) {
        if (_canEasyJump) {
            _jumpPerformed = 1;
        } else if (_canMediumJump) {
            _jumpPerformed = 2;
        } else if (_canHardJump) {
            _jumpPerformed = 3;
        }
    }

    private void JumpCompleted(GameEvent evt) {
        float jumpPoint = 0f;

        if (_jumpPerformed == 1) {
            jumpPoint = 300f;
        } else if (_jumpPerformed == 2) {
            jumpPoint = 600f;
        } else if (_jumpPerformed == 3) {
            jumpPoint = 1000f;
        }

        _jumpPerformed = 0;
        setPoints(points + ((int)jumpPoint * _multiplier));
    }

    private void JumpFailed(GameEvent evt) {
        _jumpPerformed = 0;
        _DamageTakenEvent.Invoke();
    }
}
