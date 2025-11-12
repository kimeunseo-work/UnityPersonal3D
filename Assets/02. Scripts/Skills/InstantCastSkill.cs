using System;
using UnityEngine;

[Serializable]
public class InstantCastSKill : Skill
{
    public override string SkillName => _skillName;
    public override float CoolDown => _coolDown;
    public override Sprite Icon => _icon;

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }
}