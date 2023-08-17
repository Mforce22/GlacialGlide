using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _IdProvider;




    [SerializeField]
    private float _Velocity;


    private int _direction = 1;//1 = left, -1 = right


    private PlayerControls _gameplayInputProvider;
    private PlayerTouchController _playerTouchController;

    private Vector3 _movement;
    private bool _isMoving = false;

    private void Awake()
    {
        _gameplayInputProvider = PlayerController.Instance.GetInput<PlayerControls>(_IdProvider.Id);
        _playerTouchController = new PlayerTouchController();
    }

    private void OnEnable()
    {
        _playerTouchController.Enable();

        _gameplayInputProvider.OnTouch += MoveCharacter;
        _gameplayInputProvider.OnStopTouch += StopMoving;
    }
    private void OnDisable()
    {
        _playerTouchController.Disable();
        _gameplayInputProvider.OnTouch -= MoveCharacter;
        _gameplayInputProvider.OnStopTouch -= StopMoving;
    }

    private void Update()
    {
        if (_isMoving)
        {
            if (_direction == 1)
            {
                transform.position -= new Vector3(_Velocity * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(_Velocity * Time.deltaTime, 0, 0);
            }
        }
    }

    private void StopMoving()
    {
        _isMoving = false;
    }

    private void MoveCharacter()
    {
        // Vector2 position = _playerTouchController.Touch.PrimaryPosition.ReadValue<Vector2>();
        // Vector3 touchPosition = Camera.main.ScreenToWorldPoint(position);

        Touch touch = Input.GetTouch(0);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //Debug.LogFormat("Touch Position: {0}", touchPosition);
        if (touchPosition.x < 0)
        {
            //transform.position -= new Vector3(_Velocity * Time.deltaTime, 0, 0);
            if (_direction == -1)
            {
                _direction = 1;
                transform.localScale = new Vector3(_direction, 1, 1);
            }
        }
        else if (touchPosition.x > 0)
        {
            //transform.position += new Vector3(_Velocity * Time.deltaTime, 0, 0);
            if (_direction == 1)
            {
                _direction = -1;
                transform.localScale = new Vector3(_direction, 1, 1);
            }
        }
        _isMoving = true;
        //Debug.LogFormat("Value: {0}", value);
    }

}
