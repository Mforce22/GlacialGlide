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
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }


    //Velocity Test
    [ContextMenu("InvokeEvent")]
    public void InvokeEvent()
    {
        setVelocity(10);
    }
}
