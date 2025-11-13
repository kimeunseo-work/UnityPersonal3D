using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillButton<T> : MonoBehaviour where T : Skill
{
    [SerializeField] protected T _skill;
    [SerializeField] protected Image _icon;
    [SerializeField] protected Image _cooldownImage;

    protected float _timer = 0f;
    protected float _cooldown;

    protected void Start()
    {
        _icon.sprite = _skill.Icon;
        _cooldown = _skill.Cooldown;
    }

    public abstract void OnClick();

    protected IEnumerator Cooldown()
    {
        _timer = _cooldown;
        _cooldownImage.fillAmount = 1f;

        while (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            _cooldownImage.fillAmount = _timer / _cooldown;
            yield return null;
        }

        _cooldownImage.fillAmount = 0f;
    }
}
