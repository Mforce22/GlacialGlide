using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>, ISystem
{
    [Header("System Settings")]
    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

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

    #region variables

    [Header("Game Master Settings")]

    [SerializeField]
    [Tooltip("Starting velocity of the game")]
    private int velocity;

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
    #endregion


    public void setVelocity(int _velocity)
    {
        velocity = _velocity;
        _SpeedChangeEvent.Invoke();
    }
    public int getVelocity()
    {
        return velocity;
    }

    public void setPause(bool paused)
    {
        isPaused = paused;
    }
    public bool getPause()
    {
        return isPaused;
    }

    public void setHearts(int _hearts)
    {
        hearts = _hearts;
    }
    public int getHearts()
    {
        return hearts;
    }

    public void setPoints(int _points)
    {
        points = _points;
    }
    public int getPoints()
    {
        return points;
    }

    public void Setup()
    {
        //subscibe to the event
        _ShieldHitEvent.Subscribe(ShieldHit);
        _HeartTakenEvent.Subscribe(HeartTaken);
        _CoinTakenEvent.Subscribe(CoinTaken);
        _DamageTakenEvent.Subscribe(DamageTaken);
        _X2TakenEvent.Subscribe(X2Taken);

        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    private void OnDisable()
    {
        _ShieldHitEvent.Unsubscribe(ShieldHit);
        _HeartTakenEvent.Unsubscribe(HeartTaken);
        _CoinTakenEvent.Unsubscribe(CoinTaken);
        _DamageTakenEvent.Unsubscribe(DamageTaken);
        _X2TakenEvent.Unsubscribe(X2Taken);
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
    void ShieldHit(GameEvent evt)
    {
        Debug.Log("Shield gained");
        if (!_hasShield)
        {
            _hasShield = true;
        }
    }

    void HeartTaken(GameEvent evt)
    {
        Debug.Log("Heart taken");

        if (hearts < 3)
        {
            hearts++;
        }
        //hearts++;
    }

    void CoinTaken(GameEvent evt)
    {
        Debug.Log("Coin taken");
        setPoints(points + (100 * _multiplier));
    }

    void DamageTaken(GameEvent evt)
    {
        Debug.Log("Damage taken");
        if (_hasShield)
        {
            _hasShield = false;
        }
        else
        {
            setHearts(hearts - 1);
            if (hearts <= 0)
            {
                //game over
                GameOver();
            }
        }
    }

    void X2Taken(GameEvent evt)
    {
        Debug.Log("X2 taken");
        if (_multiplier == 1)
        {
            _multiplier++;
            //Debug.Log("Multiplier taken ");
            _multiplierTimer = 10;
            StartCoroutine(ResetMultiplier());
        }
    }

    private IEnumerator ResetMultiplier()
    {
        while (_multiplierTimer > 0)
        {
            yield return new WaitForSeconds(1);
            _multiplierTimer--;
            //Debug.Log("Multiplier timer: " + _multiplierTimer);
        }
        //yield return new WaitForSeconds(10);
        _multiplier = 1;

        //Debug.Log("Multiplier reset");
    }



    //Gameover
    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    //Velocity Test
    [ContextMenu("InvokeEvent")]
    public void InvokeEvent()
    {
        setVelocity(10);
    }
}
