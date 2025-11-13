public class DetectiveButton : SkillButton<DetectiveSkill>
{
    public override void OnClick()
    {
        if (_timer > 0f) return;

        StartCoroutine(_skill.Activate());
        StartCoroutine(Cooldown());
    }
}