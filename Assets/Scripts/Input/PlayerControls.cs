using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class represents the player controls and input handling for touch-based gameplay interactions.
/// </summary>
public class PlayerControls : InputProvider
{
    #region Delegate
    /// <summary>
    /// Delegate for touch input event.
    /// </summary>
    public OnVoidDelegate OnTouch;

    /// <summary>
    /// Delegate for touch release event.
    /// </summary>
    public OnVoidDelegate OnStopTouch;
    #endregion

    [Header("Gameplay")]
    [SerializeField]
    private InputActionReference _Touch;

    [SerializeField]
    private InputActionReference _StopTouch;


    /// <summary>
    /// Enables the touch and stop touch actions and subscribes to their performed events.
    /// </summary>
    private void OnEnable() {
        // Enable the touch and stop touch actions.
        _Touch.action.Enable();
        _StopTouch.action.Enable();

        // Subscribe to the performed event for touch and stop touch actions.
        _Touch.action.performed += StartTouchPrimary;
        _StopTouch.action.performed += EndTouchPrimary;
    }

    /// <summary>
    /// Disables the touch and stop touch actions and unsubscribes from their performed events.
    /// </summary>
    private void OnDisable() {
        // Disable the touch and stop touch actions.
        _Touch.action.Disable();
        _StopTouch.action.Disable();

        // Unsubscribe from the performed event for touch and stop touch actions.
        _Touch.action.performed -= StartTouchPrimary;
        _StopTouch.action.performed -= EndTouchPrimary;
    }

    /// <summary>
    /// Handles the start of the primary touch input and invokes the OnTouch event.
    /// </summary>
    /// <param name="ctx">The InputAction.CallbackContext containing information about the input action.</param>
    private void StartTouchPrimary(InputAction.CallbackContext ctx) {
        // Invoke the OnTouch event to notify subscribers about the start of touch input.
        OnTouch?.Invoke();
    }

    /// <summary>
    /// Handles the end of the primary touch input and invokes the OnStopTouch event.
    /// </summary>
    /// <param name="ctx">The InputAction.CallbackContext containing information about the input action.</param>
    private void EndTouchPrimary(InputAction.CallbackContext ctx) {
        // Invoke the OnStopTouch event to notify subscribers about the end of touch input.
        OnStopTouch?.Invoke();
    }
}
