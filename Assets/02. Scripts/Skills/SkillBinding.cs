using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/SkillBinding")]
public class SkillBinding : ScriptableObject
{
    public KeyCode key;           // 입력 키
    public Skill skill;       // 실행할 스킬
}