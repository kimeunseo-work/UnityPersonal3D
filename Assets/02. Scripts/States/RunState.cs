using UnityEngine;

public class RunState<T> : IState
{
    public void Enter()
    {
        Debug.Log($"[{typeof(T).Name}] Start Run");
    }
    public void Update()
    {
        // 효과음 & 애니메이션 호출
    }
    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Exit Run");
    }
}