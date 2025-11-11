using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : BaseInput
{
    [Header("Player")]
    [SerializeField] Player _player;
    [SerializeField] Rigidbody _rb;

    Vector2 input;// 현재 방향

    Vector2 prevInput;// 마지막 키입력
    float lastKeyInputTime;// 마지막 키입력 시간
    const float doubleTapThreshold = 0.2f;// 연속 키입력 기준 시간



    private void Reset()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Movement();
    }



    public override void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("OnAttack");
    }

    public override void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump");
    }

    public override void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            input = context.ReadValue<Vector2>();

            // 멈춤 판정이 아니라면
            if (input != Vector2.zero)
            {
                // 같은 키 && 더블탭 간격이 기준에 적합
                if (input == prevInput
                    && Time.time - lastKeyInputTime < doubleTapThreshold)
                {
                    _player.ChangeMoveState(_player.RunState);
                }

                // 마지막 키 정보 할당
                prevInput = input;
                lastKeyInputTime = Time.time;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _player.ChangeMoveState(_player.WalkState);
            input = Vector2.zero;
        }
    }



    void Movement()
    {
        Vector3 dir = transform.forward * input.y
                    + transform.right * input.x;

        dir *= _player.MoveSpeed;
        dir.y = _rb.velocity.y;

        _rb.velocity = dir;
    }
}