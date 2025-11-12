using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : BaseInput
{
    Player _player;

    public Vector2 Input { get; private set; }// 현재 방향

    Vector2 prevInput;// 마지막 키입력
    float lastKeyInputTime;// 마지막 키입력 시간
    const float doubleTapThreshold = 0.2f;// 연속 키입력 기준 시간



    private void Awake()
    {
        _player = GetComponent<Player>();
    }



    public override void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("OnAttack");
    }

    public override void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Input = context.ReadValue<Vector2>();
            _player.ChangeMoveState(_player.WalkState);

            // 멈춤 판정이 아니라면
            if (Input != Vector2.zero)
            {
                // 같은 키 && 더블탭 간격이 기준에 적합
                if (Input == prevInput
                    && Time.time - lastKeyInputTime < doubleTapThreshold)
                {
                    _player.ChangeMoveState(_player.RunState);
                }

                // 마지막 키 정보 할당
                prevInput = Input;
                lastKeyInputTime = Time.time;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _player.ChangeMoveState(_player.IdleState);
            Input = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (_player.MoveState.CurrentState is not JumpState<Player>
            && context.phase == InputActionPhase.Started)
        {
            _player.ChangeMoveState(_player.JumpState);
        }
    }

    public void OnDetect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _player.Detect();
        }
    }
}