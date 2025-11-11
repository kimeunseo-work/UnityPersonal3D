using UnityEngine;

public class IdleState<T> : IState
{
    private readonly Player _owner;

    public IdleState(Player owner)
    {
        this._owner = owner;
    }

    public void Enter()
    {
        Debug.Log($"[{typeof(T).Name}] Start Idle");
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Start Idle");
    }
}
