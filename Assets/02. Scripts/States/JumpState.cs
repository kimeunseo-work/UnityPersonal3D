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
    }

    public void Update()
    {
        if (_owner.Rigidbody.velocity.y < 0)
            _owner.ChangeAirborneState(_owner.FallState);
    }

    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Exit Jump");
    }
}