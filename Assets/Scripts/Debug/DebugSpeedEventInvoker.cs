using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpeedEventInvoker : MonoBehaviour
{
    [SerializeField]
    private GameEvent _DebugEvent;

    [ContextMenu("InvokeEvent")]
    public void InvokeEvent()
    {
        _DebugEvent.Invoke();
    }
}
