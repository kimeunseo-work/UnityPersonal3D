using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    [SerializeField] private List<SkillBinding> bindings;

    private readonly Dictionary<KeyCode, SkillBinding> bindingMap = new();
    private readonly Dictionary<KeyCode, float> cooldownTimers = new();

    private void Awake()
    {
        foreach (var bind in bindings)
        {
            if (bind == null || bind.skill == null) continue;

            bindingMap[bind.key] = bind;
            cooldownTimers[bind.key] = 0f;
        }
    }

    private void Update()
    {
        /*쿨타임 감소*/
        var keys = new List<KeyCode>(cooldownTimers.Keys);
        foreach (var key in keys)
        {
            if (cooldownTimers[key] > 0f)
                cooldownTimers[key] -= Time.deltaTime;
        }
    }

    public void TryUseSkill(KeyCode key, Player target)
    {
        if (!bindingMap.ContainsKey(key)) return;

        var binding = bindingMap[key];
        var skill = binding.skill;

        if (cooldownTimers[key] > 0f)
        {
            Debug.Log($"[{gameObject.name}] {skill.SkillName} is Cool");
            return;
        }

        StartCoroutine(skill.Activate(target));
        cooldownTimers[key] = skill.Cooldown;
    }
}
