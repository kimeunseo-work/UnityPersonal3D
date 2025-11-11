using UnityEngine;

public abstract class Chracter : MonoBehaviour
{
    public abstract int Hp { get; protected set; }
    public abstract int MaxHp { get; protected set; }
    public abstract void TakeDamage();
    protected abstract void DealDamage();
    protected abstract void Move();
    protected abstract void Die();
}
