using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : InputProvider
{


    #region Delegate
    public OnVoidDelegate OnTouch;
    public OnVoidDelegate OnStopTouch;
    #endregion

    [Header("Gameplay")]
    [SerializeField]
    private InputActionReference _Touch;

    [SerializeField]
    private InputActionReference _StopTouch;


    private void OnEnable()
    {
        _Touch.action.Enable();
        _StopTouch.action.Enable();

        _Touch.action.performed += StartTouchPrimary;
        _StopTouch.action.performed += EndTouchPrimary;
    }

    private void OnDisable()
    {
        _Touch.action.Disable();
        _StopTouch.action.Disable();

        _Touch.action.performed -= StartTouchPrimary;
        _StopTouch.action.performed -= EndTouchPrimary;
    }

    private void Start()
    {
        // _playerTouchController.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        // _playerTouchController.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnTouch?.Invoke();
        //Debug.Log("Start Touch");
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStopTouch?.Invoke();
        //Debug.Log("End Touch");
    }
}
