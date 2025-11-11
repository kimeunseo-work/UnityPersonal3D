using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract int Hp { get; protected set; }
    public abstract int MaxHp { get; protected set; }
    public abstract void TakeDamage(int amount);
    public abstract void DealDamage();
    public abstract void Move(Vector2 input);
    public abstract void Die();
}
