using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator _animator;

    [Header("Events")]
    [SerializeField]
    private GameEvent _jumpStartEvent;

    [SerializeField]
    private GameEvent _jumpEndEvent;

    [SerializeField]
    private GameEvent _jumpFailedEvent;

    [SerializeField]
    private GameEvent _damageTakenEvent;

    [SerializeField]
    private GameEvent _bombEvent;

    private void OnEnable()
    {
        _jumpStartEvent.Subscribe(OnJumpStart);
        _jumpEndEvent.Subscribe(OnJumpEnd);
        _jumpFailedEvent.Subscribe(OnDamageTaken);
        _damageTakenEvent.Subscribe(OnDamageTaken);
        _bombEvent.Subscribe(OnDamageTaken);
    }

    private void OnDisable()
    {
        _jumpStartEvent.Unsubscribe(OnJumpStart);
        _jumpEndEvent.Unsubscribe(OnJumpEnd);
        _jumpFailedEvent.Unsubscribe(OnDamageTaken);
        _damageTakenEvent.Unsubscribe(OnDamageTaken);
        _bombEvent.Unsubscribe(OnDamageTaken);
    }

    private void OnJumpStart(GameEvent evt)
    {
        _animator.SetTrigger("StartJump");
    }

    private void OnJumpEnd(GameEvent evt)
    {
        _animator.SetTrigger("JumpEnded");
    }

    private void OnDamageTaken(GameEvent evt)
    {
        _animator.SetTrigger("Damage");
    }
}
