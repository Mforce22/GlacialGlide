using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField]
    private IdContainer _IdProvider;

    [SerializeField]
    private GameEvent _pauseEvent;

    [Header("Swipe Settings")]
    [SerializeField]
    private float _swipeDistance;

    [SerializeField]
    private float _maxSwipeTime;

    [SerializeField, Range(0f, 1f)]
    private float _directionTreshold = 0.9f;


    private PlayerControls _gameplayInputProvider;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float _startTime;
    private float _endTime;

    private bool _isPaused = false;

    private void Awake()
    {
        _gameplayInputProvider = PlayerController.Instance.GetInput<PlayerControls>(_IdProvider.Id);
        //_playerTouchController = new PlayerTouchController();
    }


    private void OnEnable()
    {
        //_playerTouchController.Enable();

        _gameplayInputProvider.OnTouch += StartMoving;
        _gameplayInputProvider.OnStopTouch += StopMoving;

        _pauseEvent.Subscribe(Pause);
    }
    private void OnDisable()
    {
        //_playerTouchController.Disable();
        _gameplayInputProvider.OnTouch -= StartMoving;
        _gameplayInputProvider.OnStopTouch -= StopMoving;

        _pauseEvent.Unsubscribe(Pause);
    }


    private void StartMoving()
    {
        Touch touch = Input.GetTouch(Input.touchCount - 1);
        startPosition = Camera.main.ScreenToWorldPoint(touch.position);

        _startTime = Time.time;

    }
    private void StopMoving()
    {
        Touch touch = Input.GetTouch(Input.touchCount - 1);
        endPosition = Camera.main.ScreenToWorldPoint(touch.position);

        _endTime = Time.time;

        if (!_isPaused)
        {
            bool check = CheckSwipe();
            if (check)
            {
                //Debug.Log("Swipe Detected");
            }
        }
    }

    private void Pause(GameEvent evt)
    {
        _isPaused = !_isPaused;
    }

    private bool CheckSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= _swipeDistance && (_endTime - _startTime) <= _maxSwipeTime)
        {
            Debug.Log("Swipe Detected");
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            if (Vector2.Dot(direction2D, Vector2.up) > _directionTreshold)
            {
                Debug.Log("Swipe Up");
                return true;
            }
        }

        return false;
    }
}
