using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputProvider : InputProvider
{
    #region Delegate
    public OnFloatDelegate OnTouch;
    #endregion

    [Header("Gameplay")]
    [SerializeField]
    private InputActionReference _Touch;


    private void OnEnable()
    {
        _Touch.action.Enable();

        _Touch.action.performed += MovePerfomed;
    }

    private void OnDisable()
    {
        _Touch.action.Disable();

        _Touch.action.performed -= MovePerfomed;
    }

    private void MovePerfomed(InputAction.CallbackContext obj)
    {
        float value = obj.action.ReadValue<float>();
        OnTouch?.Invoke(value);
    }
}
