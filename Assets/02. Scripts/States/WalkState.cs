using UnityEngine;
public class WalkState<T> : IState
{
    public void Enter()
    {
        Debug.Log($"[{typeof(T).Name}] Start Walk");
    }
    public void Update()
    {
        // 효과음 & 애니메이션 호출
    }
    public void Exit()
    {
        Debug.Log($"[{typeof(T).Name}] Exit Walk");
    }
}