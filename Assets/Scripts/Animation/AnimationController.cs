using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField]
    private Animator _Animator;

    [Header("Events")]
    [SerializeField]
    private GameEvent _JumpStartEvent;

    [SerializeField]
    private GameEvent _JumpEndEvent;

    [SerializeField]
    private GameEvent _JumpFailedEvent;

    [SerializeField]
    private GameEvent _DamageTakenEvent;

    [SerializeField]
    private GameEvent _BombEvent;


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
        _JumpStartEvent.Subscribe(OnJumpStart);
        _JumpEndEvent.Subscribe(OnJumpEnd);
        _JumpFailedEvent.Subscribe(OnDamageTaken);
        _DamageTakenEvent.Subscribe(OnDamageTaken);
        _BombEvent.Subscribe(OnDamageTaken);
    }

    private void OnDisable()
    {
        _JumpStartEvent.Unsubscribe(OnJumpStart);
        _JumpEndEvent.Unsubscribe(OnJumpEnd);
        _JumpFailedEvent.Unsubscribe(OnDamageTaken);
        _DamageTakenEvent.Unsubscribe(OnDamageTaken);
        _BombEvent.Unsubscribe(OnDamageTaken);
    }

    private void OnJumpStart(GameEvent evt)
    {
        if (GameMaster.Instance.getHardJump())
        {
            _Animator.SetTrigger("StartBigJump");
            _currentAnimationState = AnimationState.BigJump;
        }
        else
        {
            _Animator.SetTrigger("StartJump");
            _currentAnimationState = AnimationState.Jump;
        }
    }

    private void OnJumpEnd(GameEvent evt)
    {
        if (_currentAnimationState == AnimationState.BigJump)
        {
            _Animator.SetTrigger("EndBigJump");
        }
        else
        {
            _Animator.SetTrigger("JumpEnded");
        }
        _currentAnimationState = AnimationState.None;
    }

    private void OnDamageTaken(GameEvent evt)
    {
        _Animator.SetTrigger("Damage");
    }
}
