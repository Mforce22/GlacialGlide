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


    //create an enum for the animation states
    private enum AnimationState
    {
        None,
        Jump,
        BigJump
    }

    private AnimationState _currentAnimationState = AnimationState.None;
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
        if (GameMaster.Instance.getHardJump())
        {
            _animator.SetTrigger("StartBigJump");
            _currentAnimationState = AnimationState.BigJump;
        }
        else
        {
            _animator.SetTrigger("StartJump");
            _currentAnimationState = AnimationState.Jump;
        }
    }

    private void OnJumpEnd(GameEvent evt)
    {
        if (_currentAnimationState == AnimationState.BigJump)
        {
            _animator.SetTrigger("EndBigJump");
        }
        else
        {
            _animator.SetTrigger("JumpEnded");
        }
        _currentAnimationState = AnimationState.None;
    }

    private void OnDamageTaken(GameEvent evt)
    {
        _animator.SetTrigger("Damage");
    }
}
