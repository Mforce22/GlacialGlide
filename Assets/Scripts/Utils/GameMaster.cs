using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>
{
    #region variables

    [Header("Game Master Settings")]

    [SerializeField]
    [Tooltip("Starting velocity of the game")]
    private int velocity;

    [SerializeField]
    [Tooltip("Is the game paused?")]
    public bool isPaused;
    #endregion

    //assign variables to local variiable

    int _velocity;


    public int Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }


    private void Awake()
    {
        _velocity = velocity;
    }
}
