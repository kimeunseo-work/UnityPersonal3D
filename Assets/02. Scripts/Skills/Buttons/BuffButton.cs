using UnityEngine;

public class BuffButton : SkillButton<Skill>
{
    [SerializeField] Player player;

    public override void OnClick()
    {
        if (_timer > 0f) return;

        StartCoroutine(_skill.Activate(player));
        StartCoroutine(Cooldown());
    }
}