using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/InstantHealSkill")]
public class InstantHealSkill : Skill
{
    [SerializeField] AffectedStat[] _affectedStats;
    public override string SkillName => _skillName;
    public override float Cooldown => _cooldown;
    public override Sprite Icon => _icon;
    public AffectedStat[] AffectedStats => _affectedStats;

    public override IEnumerator Activate(Player target)
    {
        foreach (AffectedStat stat in _affectedStats)
        {
            Debug.Log($"[BuffSkill] stat {stat.Stat}");
            if (target.StatModifyActions
                .TryGetValue(stat.Stat.Stat, out var action)
            )
            {
                action(stat.Value, true);
            }
        }

        yield return null;
    }
}
