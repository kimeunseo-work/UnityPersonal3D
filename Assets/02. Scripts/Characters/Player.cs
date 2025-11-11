using UnityEngine;

public class Player : Chracter
{
    #region SerializeField
    [Header("Player")]
    [SerializeField] SO_PlayerData _data;
    [SerializeField] BaseInput _input;
    #endregion

    #region Status Fields
    private int _hp;
    private int _maxHp;
    private int _stamina;
    private int _maxStamina;
    private int _atk;
    private int _speedWeight;
    private int _jumpWeight;
    #endregion

    #region Status Properties
    public override int Hp
    {
        get => _hp;
        protected set => _hp = value;
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
    public int SpeedWeight
    {
        get => _speedWeight;
        private set => _speedWeight = value;
    }
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
    public int JumpMoveSpeed
    {
        get => _data.JumpMoveSpeed;
    }
    public int MoveSpeed
    {
        get => MoveState.CurrentState is WalkState<Player>
            ? WalkSpeed
            : RunSpeed;
    }
    #endregion

    #region StateMachine Properties
    public StateMachine<Player> MoveState { get; private set; }
    #endregion

    #region IState Properties
    public IState WalkState { get; private set; }
    public IState RunState { get; private set; }
    #endregion

    #region LifeCycle
    private void Awake()
    {
        MoveState = new StateMachine<Player>();
        WalkState = new WalkState<Player>();
        RunState = new RunState<Player>();
    }

    private void Reset()
    {
        _input = GetComponent<BaseInput>();
    }

    private void Start()
    {
        /*Status*/
        Hp = _data.Hp;
        MaxHp = _data.Hp;
        Stamina = _data.Stamina;
        MaxStamina = _data.Stamina;
        Atk = _data.Atk;
        SpeedWeight = 0;
        JumpWeight = 0;
        /* // Status
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
        MoveState.Initialize(WalkState);
    }

    private void Update()
    {
        /*States*/
        MoveState.Update();
    }
    #endregion


    public override void TakeDamage()
    {
        throw new System.NotImplementedException();
    }

    protected override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    private void Jump()
    {

    }

    #region ChangeState API
    public void ChangeMoveState(IState newState)
    {
        if (MoveState.CurrentState == newState) return;
        MoveState.ChangeState(newState);
    }
    #endregion
}