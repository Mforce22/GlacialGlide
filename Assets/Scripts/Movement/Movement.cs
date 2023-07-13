using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Events")]
    [Tooltip("Event to subscribe to change the speed of the object")]
    [SerializeField]
    private GameEvent _ChangeSpeedEvent;

    [Header("Movement")]
    private int _movement;
    // Start is called before the first frame update
    void Start()
    {
        //get instance of GameMaster
        _movement = GameMaster.Instance.getVelocity();
        _ChangeSpeedEvent.Subscribe(SpeedChanged);
    }

    // Update is called once per frame
    void Update()
    {
        //if the game isn't in pause
        if (!GameMaster.Instance.getPause())
        {
            //move up 
            transform.Translate(Vector3.up * _movement * Time.deltaTime);
        }
    }

    void SpeedChanged(GameEvent evt)
    {
        _movement = GameMaster.Instance.getVelocity();
        Debug.Log("Speed changed to " + _movement);
    }
}
