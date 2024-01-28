
using System;
using UnityEngine;

public readonly struct RequestMoveAnimationEvent : IEvent
{
    public readonly float Speed;

    public RequestMoveAnimationEvent(float speed)
    {
        Speed = speed;
    }
}

public readonly struct RequestJumpAnimationEvent : IEvent
{
    public readonly float JumpSpeed;
    public readonly bool IsGrounded;

    public RequestJumpAnimationEvent(float jumpSpeed, bool isGrounded)
    {
        JumpSpeed = jumpSpeed;
        IsGrounded = isGrounded;
    }
}

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorManager : MonoBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int JumpSpeed = Animator.StringToHash("JumpSpeed");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private bool _isGameRunning;

    private Animator _animator => GetComponent<Animator>();

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Dispose();
    }

    private void Initialize()
    {
        new ResponseGameStateUpdateEvent().AddListener(HandlerRequestNewGameStateEvent);
        new RequestMoveAnimationEvent().AddListener(HandlerRequestMoveAnimationEvent);
        new RequestJumpAnimationEvent().AddListener(HandlerRequestJumpAnimationEvent);
    }

    private void HandlerRequestNewGameStateEvent(ResponseGameStateUpdateEvent e)
    {
        _animator.enabled = e.CurrentGameState.Equals(GameStates.GameRunning);
    }

    private void HandlerRequestMoveAnimationEvent(RequestMoveAnimationEvent e)
    {
        if (e.Speed != 0 && _animator.enabled)
        {
            transform.localScale = new Vector3(e.Speed < 0 ? -1 : 1, transform.localScale.y);
        }

        _animator.SetFloat(Move, Mathf.Abs(e.Speed));
    }

    private void HandlerRequestJumpAnimationEvent(RequestJumpAnimationEvent e)
    {
        _animator.SetFloat(JumpSpeed, e.JumpSpeed);
        _animator.SetBool(IsGrounded, e.IsGrounded);
    }

    private void Dispose()
    {
        new RequestMoveAnimationEvent().RemoveListener(HandlerRequestMoveAnimationEvent);
    }
}
