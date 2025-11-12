using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player")]
    [SerializeField] PlayerInput _input;
    [SerializeField] SO_PlayerData _data;
    [SerializeField] SkillHandler _skillHandler;

    [Header("Movement")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] LayerMask _groundLayerMask;

    public PlayerInput Input => _input;
    public Rigidbody Rigidbody => _rb;
    public LayerMask GroundLayerMask => _groundLayerMask;

    /*Status*/
    //=======================================================//

    #region Status Fields: 프로퍼티 구성용
    private int _hp;
    private int _maxHp;
    private int _stamina;
    private int _maxStamina;
    private int _atk;
    private int _speedWeight;
    private int _jumpWeight;
    #endregion

    #region Status Properties: 스탯 Get 전용
    public override int Hp
    {
        get => _hp;
        protected set
        {
            if (_hp != value)
            {
                _hp = Mathf.Clamp(
                    value,
                    StatConstants.MinHP,
                    MaxHp
                );
                OnHpChanged?.Invoke(value, MaxHp);
            }
        }
    }
    public override int MaxHp
    {
        get => _maxHp;
        protected set => _maxHp = value;
    }
    public int Stamina
    {
        get => _stamina;
        private set => _stamina = value;
    }
    public int MaxStamina
    {
        get => _maxStamina;
        private set => _maxStamina = value;
    }
    public int Atk
    {
        get => _atk;
        private set => _atk = value;
    }
    /// <summary>
    /// 이동 속도 조절에 사용
    /// </summary>
    public int SpeedWeight
    {
        get => _speedWeight;
        private set => _speedWeight = value;
    }
    /// <summary>
    /// 점프 파워 조절에 사용
    /// </summary>
    public int JumpWeight
    {
        get => _jumpWeight;
        private set => _jumpWeight = value;
    }
    public int WalkSpeed
    {
        get => _data.WalkSpeed + SpeedWeight;
    }
    public int RunSpeed
    {
        get => _data.RunSpeed + SpeedWeight;
    }
    public int JumpPower
    {
        get => _data.JumpPower + JumpWeight;
    }
    /// <summary>
    /// 점프 중 방향 이동 속도 (미구현)
    /// </summary>
    public int JumpMoveSpeed
    {
        get => _data.JumpMoveSpeed;
    }
    /// <summary>
    /// 계산에 사용되는 이동 속도
    /// </summary>
    public int MoveSpeed
    {
        get => GroundedState.CurrentState is WalkState<Player>
            ? WalkSpeed
            : RunSpeed;
    }
    #endregion

    #region StatModity Actions: 직접 호출 X
    /*함수를 추가 할 경우 Awake에서 등록 필수*/

    public void ModifyHp(int amount, bool isPositive = true)
        => Hp = isPositive ? amount : -amount;

    public void ModifyStamina(int amount, bool isPositive = true)
        => Stamina = isPositive ? amount : -amount;

    public void ModifyAtk(int amount, bool isPositive = true)
        => Atk = isPositive ? amount : -amount;

    public void ModifySpeedWeight(int amount, bool isPositive = true)
        => SpeedWeight = isPositive ? amount : -amount;

    public void ModifyJumpWeight(int amount, bool isPositive = true)
        => JumpWeight = isPositive ? amount : -amount;

    #endregion

    /// <summary>
    /// 현재 용도: 스탯 관련 스킬에서 스탯 판별용으로 사용
    /// </summary>
    public Dictionary<Enum.Stats, Action<int, bool>> StatModifyActions;


    /*State*/
    //=======================================================//

    #region StateMachine Properties 
    public StateMachine<Player> GroundedState { get; private set; }
    public StateMachine<Player> AirborneState { get; private set; }
    #endregion

    #region IState Properties
    /*추가 후 Awake()에서 초기화 필요*/
    public IState IdleState { get; private set; }
    public IState WalkState { get; private set; }
    public IState RunState { get; private set; }

    public IState JumpState { get; private set; }
    public IState FallState { get; private set; }
    #endregion


    /*Event*/
    //=======================================================//

    /// <summary>
    /// 착지 이벤트
    /// </summary>
    public event Action OnLanded;
    public event Action<int, int> OnHpChanged;


    /*LifeCycle*/
    //=======================================================//


    #region LifeCycle
    private void Awake()
    {
        /*StateMachine*/
        GroundedState = new StateMachine<Player>();
        AirborneState = new StateMachine<Player>();

        /*GroundedState*/
        IdleState = new IdleState<Player>(this);
        WalkState = new WalkState<Player>(this);
        RunState = new RunState<Player>(this);

        /*AirborneState*/
        JumpState = new JumpState<Player>(this);
        FallState = new FallState<Player>(this);

        StatModifyActions =
        new Dictionary<Enum.Stats, Action<int, bool>>()
        {
            { Enum.Stats.Hp, ModifyHp },
            { Enum.Stats.Stamina, ModifyStamina },
            { Enum.Stats.Atk, ModifyAtk },
            { Enum.Stats.SpeedWeight, ModifySpeedWeight },
            { Enum.Stats.JumpWeight, ModifyJumpWeight },
        };
    }

    private void Reset()
    {
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        /*Status*/
        MaxHp = _data.Hp;
        Hp = _data.Hp;
        Stamina = _data.Stamina;
        MaxStamina = _data.Stamina;
        Atk = _data.Atk;
        SpeedWeight = 0;
        JumpWeight = 0;
        /*        // Status
                Debug.Log($"[{gameObject.name}] Hp {Hp}");
                Debug.Log($"[{gameObject.name}] MaxHp {MaxHp}");
                Debug.Log($"[{gameObject.name}] Stamina {Stamina}");
                Debug.Log($"[{gameObject.name}] MaxStamina {MaxStamina}");
                Debug.Log($"[{gameObject.name}] Atk {Atk}");
                Debug.Log($"[{gameObject.name}] SpeedWeight {SpeedWeight}");
                Debug.Log($"[{gameObject.name}] JumpWeight {JumpWeight}");
                Debug.Log($"[{gameObject.name}] WalkSpeed {WalkSpeed}");
                Debug.Log($"[{gameObject.name}] RunSpeed {RunSpeed}");
                Debug.Log($"[{gameObject.name}] JumpPower {JumpPower}");
                Debug.Log($"[{gameObject.name}] JumpMoveSpeed {JumpMoveSpeed}");*/

        /*States*/
        GroundedState.Initialize(WalkState);
    }

    private void Update()
    {
        /*States*/
        GroundedState?.Update();
        AirborneState?.Update();
    }

    #endregion


    /*Change*/
    //=======================================================//


    #region ChangeState API
    public void ChangeMoveState(IState newState)
    {
        if (GroundedState.CurrentState == newState) return;
        GroundedState.ChangeState(newState);
    }
    public void ChangeAirborneState(IState newState)
    {
        if (AirborneState.CurrentState == newState) return;
        AirborneState.ChangeState(newState);
    }
    #endregion

    public void SetJumpWeight(int amount)
    {
        //Debug.Log($"[{gameObject.name}] SetJumpWeight");
        JumpWeight = amount;
    }



    /*Move*/
    //=======================================================//


    #region Move
    public override void TryWalk()
    {
        ChangeMoveState(WalkState);
    }

    public override void TryRun()
    {
        if (AirborneState == null)
            ChangeMoveState(RunState);
    }

    public void Move(Vector2 input)
    {
        Vector3 dir = transform.forward * input.y
                    + transform.right * input.x;

        dir *= MoveSpeed;
        dir.y = Rigidbody.velocity.y;

        Rigidbody.velocity = dir;
    }
    #endregion

    #region Jump
    public void TryJump()
    {
        if (AirborneState.CurrentState is not JumpState<Player>)
            ChangeAirborneState(JumpState);
    }

    public void Jump()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
    }

    public bool IsGrounded()
    {
        float rayLength = 1f;
        Vector3 baseOffset = new(0, 0.01f, 0);
        Ray[] rays = new Ray[4]
        {
            new(transform.position + baseOffset + (transform.forward * 0.2f), Vector3.down),
            new(transform.position + baseOffset + (-transform.forward * 0.2f), Vector3.down),
            new(transform.position + baseOffset + (transform.right * 0.2f), Vector3.down),
            new(transform.position + baseOffset + (-transform.right * 0.2f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Ray ray = rays[i];

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, GroundLayerMask))
            {
                // 충돌한 지점까지 레이 표시 (초록색)
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                return true;
            }
            else
            {
                // 충돌 안 하면 레이 길이만큼 표시 (빨간색)
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.red);
            }
        }
        return false;
    }

    #endregion


    /*Combat*/
    //=======================================================//


    public override void TakeDamage(int amount)
    {
        Hp -= amount;
    }

    public override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    #region Skills
    public void Detect()
        => _skillHandler.TryUseSkill(KeyCode.A, null);

    #endregion
}