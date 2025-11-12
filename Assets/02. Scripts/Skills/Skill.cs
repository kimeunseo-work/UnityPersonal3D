using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string _skillName;
    public float _coolDown;
    public Sprite _icon;

    public abstract string SkillName { get; }
    public abstract float CoolDown{ get; }
    public abstract Sprite Icon { get; }

    public abstract void Activate();
    public virtual bool CanUse(float remainingCooldown)
        => remainingCooldown <= 0f;
}