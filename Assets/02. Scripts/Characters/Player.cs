using UnityEngine;

public class Player : Chracter
{
    [Header("Player")]
    [SerializeField] private SO_PlayerData data;
    /// <summary>
    /// 플레이어의 입력을 받아 엔티티로 전송
    /// </summary>
    [SerializeField] private BaseInput input;
    /// <summary>
    /// 상태에 따라 효과음과 애니메이션 출력
    /// </summary>
    [SerializeField] private BaseView view;

    #region Fields

    private int _hp;
    private int _maxHp;
    private int _stamina;
    private int _maxStamina;
    private int _atk;
    private int _speedWeight;
    private int _jumpWeight;

    #endregion

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
        get => data.WalkSpeed + SpeedWeight;
    }
    public int RunSpeed
    {
        get => data.RunSpeed + SpeedWeight;
    }
    public int JumpPower
    {
        get => data.JumpPower + JumpWeight;
    }
    public int JumpMoveSpeed
    {
        get => data.JumpMoveSpeed;
    }
    

    public bool IsRunning = false;
    public int MoveSpeed
    {
        get => IsRunning ? RunSpeed : WalkSpeed;
    }



    private void Reset()
    {
        input = GetComponent<BaseInput>();
        view = GetComponent<BaseView>();
    }

    private void Start()
    {
        Hp = data.Hp;
        MaxHp = data.Hp;
        Stamina = data.Stamina;
        MaxStamina = data.Stamina;
        Atk = data.Atk;
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
    }



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


}