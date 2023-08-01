using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampsCollision : MonoBehaviour
{
    [Header("Ramp Events")]
    [Tooltip("Event invoked when the player enter a ramp")]
    [SerializeField]
    private GameEvent _RampEnterEvent;

    [Tooltip("Event invoked when the player exit a ramp")]
    [SerializeField]
    private GameEvent _RampExitEvent;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{gameObject.name} collided with {collision.gameObject.name}");
        _RampEnterEvent?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{gameObject.name} exited collision with {collision.gameObject.name}");
        _RampExitEvent?.Invoke();
    }
}
