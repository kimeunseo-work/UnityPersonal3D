using UnityEngine;
public class WalkState<T> : IState
{
    private readonly Player _owner;
    public WalkState(Player owner)
    {
        this._owner = owner;
    }

    public void Enter()
    {
        //Debug.Log($"[{typeof(T).Name}] Start Walk");
    }
    public void Update()
    {
        // 효과음 & 애니메이션 호출
        // 이동 호출
        _owner.Move(_owner.Input.Input);
    }
    public void Exit()
    {
        _owner.Move(default);
        //Debug.Log($"[{typeof(T).Name}] Exit Walk");
    }
}