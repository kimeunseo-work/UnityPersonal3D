using System;
using UnityEngine;
internal class JumpState<T> : IState
{
    private readonly Player _owner;

    public JumpState(Player owner)
    {
        this._owner = owner;
    }

    public void Enter()
    {
        Debug.Log($"[{typeof(T).Name}] Start Jump");
        _owner.Jump();
        _owner.OnLanded += HandleLanded;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Exit Jump");
        _owner.OnLanded -= HandleLanded;
    }

    private void HandleLanded()
    {
        _owner.ChangeMoveState(_owner.IdleState);
    }
}