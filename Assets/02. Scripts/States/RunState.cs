using UnityEngine;

public class RunState<T> : IState
{
    private readonly Player _owner;
    public RunState(Player owner)
    {
        this._owner = owner;
    }

    public void Enter()
    {
        Debug.Log($"[{typeof(T).Name}] Start Run");
    }
    public void Update()
    {
        // 효과음 & 애니메이션 호출

        // 이동 호출
        _owner.Move(_owner.Input.Input);
    }
    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Exit Run");
    }
}