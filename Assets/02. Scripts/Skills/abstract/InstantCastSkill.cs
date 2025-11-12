using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/InstantCastSkill")]
public class InstantCastSKill : Skill
{
    public override string SkillName => _skillName;
    public override float Cooldown => _cooldown;
    public override Sprite Icon => _icon;
}