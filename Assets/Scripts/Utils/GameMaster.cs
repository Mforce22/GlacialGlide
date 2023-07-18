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

        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    private void OnDisable()
    {
        _ShieldHitEvent.Unsubscribe(ShieldHit);
        _HeartTakenEvent.Unsubscribe(HeartTaken);
        _CoinTakenEvent.Unsubscribe(CoinTaken);
    }

    //used for testing
    private void OnEnable()
    {
        _ShieldHitEvent.Subscribe(ShieldHit);
        _HeartTakenEvent.Subscribe(HeartTaken);
        _CoinTakenEvent.Subscribe(CoinTaken);
    }


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
        setPoints(points + 100);
    }


    //Velocity Test
    [ContextMenu("InvokeEvent")]
    public void InvokeEvent()
    {
        setVelocity(10);
    }
}
