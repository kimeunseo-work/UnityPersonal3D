using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] Player _owner;

    [Header("HP")]
    [SerializeField] Image _hpbar;
    [SerializeField] Image _hpState;

    private void Reset()
    {
        _owner = GetComponentInParent<Player>();
    }

    private void OnEnable()
    {
        _owner.OnHpChanged += HandleHpChanged;
    }
    private void OnDisable()
    {
        _owner.OnHpChanged -= HandleHpChanged;
    }

    private void HandleHpChanged(int hp, int maxHp)
    {
        _hpbar.fillAmount = (float)hp / maxHp;
    }
}