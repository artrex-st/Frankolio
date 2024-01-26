using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct MoveAnimationEvent : IEvent
{
    public readonly float Speed;

    public MoveAnimationEvent(float speed)
    {
        Speed = speed;
    }
}

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorManager : MonoBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");
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
        new MoveAnimationEvent().AddListener(HandlerMoveAnimationEvent);
        new RequestNewGameStateEvent().AddListener(HandlerRequestNewGameStateEvent);
    }

    private void HandlerRequestNewGameStateEvent(RequestNewGameStateEvent e)
    {
        _animator.enabled = e.CurrentGameState.Equals(GameStates.GameRunning);
    }

    private void HandlerMoveAnimationEvent(MoveAnimationEvent e)
    {
        if (e.Speed != 0 && _animator.enabled)
        {
            transform.localScale = new Vector3(e.Speed < 0 ? -1 : 1, transform.localScale.y);
        }

        _animator.SetFloat(Move, Mathf.Abs(e.Speed));
    }

    private void Dispose()
    {
        new MoveAnimationEvent().RemoveListener(HandlerMoveAnimationEvent);
    }
}
