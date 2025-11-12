using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/BuffSkill")]
public class BuffSkill : Skill
{
    [SerializeField] AffectedStat[] _affectedStats;
    [SerializeField] float _duration;
    public override string SkillName => _skillName;
    public override float Cooldown => _cooldown;
    public override Sprite Icon => _icon;
    public AffectedStat[] AffectedStats => _affectedStats;
    public float Duration => _duration;

    public override IEnumerator Activate(Player target)
    {
        foreach (AffectedStat stat in _affectedStats)
        {
            if (target.StatModifyActions
                .TryGetValue(stat.Stat.Stat, out var action)
            )
            {
                action(stat.Value, true);
            }
        }

        yield return new WaitForSeconds(Duration);

        foreach (AffectedStat stat in _affectedStats)
        {
            if (target.StatModifyActions
                .TryGetValue(stat.Stat.Stat, out var action)
            )
            {
                action(stat.Value, false);
            }
        }
    }
}

[System.Serializable]
public class AffectedStat
{
    public SkillStatSO Stat;
    public int Value;
}