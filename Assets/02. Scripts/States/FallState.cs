using UnityEngine;
internal class FallState<T> : IState
{
    private readonly Player _owner;

    public FallState(Player owner)
    {
        this._owner = owner;
    }

    public void Enter()
    {
        Debug.Log($"[{typeof(T).Name}] Start Fall");
    }

    public void Update()
    {
        if (_owner.IsGrounded())
            _owner.ChangeAirborneState(null);
    }

    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Exit Fall");
    }
}