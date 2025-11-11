using UnityEngine;

public abstract class Chracter : MonoBehaviour
{
    public abstract int Hp { get; protected set; }
    public abstract int MaxHp { get; protected set; }
    public abstract void TakeDamage();
    public abstract void DealDamage();
    public abstract void Move(Vector2 input);
    public abstract void Die();
}
