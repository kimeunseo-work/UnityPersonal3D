using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string _skillName;
    public float _cooldown;
    public Sprite _icon;

    public abstract string SkillName { get; }
    public abstract float Cooldown{ get; }
    public abstract Sprite Icon { get; }

    /*필요한 함수만 작성*/
    public virtual IEnumerator Activate() { yield break; }
    public virtual IEnumerator Activate(Character target) { yield break; }
    public virtual IEnumerator Activate(Player target) { yield break; }
}