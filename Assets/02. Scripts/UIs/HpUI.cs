using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] Player _owner;

    [Header("HP")]
    [SerializeField] Image _hpbar;
    [SerializeField] Image _hpState;

    
    Coroutine _coroutine;
    public Coroutine Coroutine
    {
        get => _coroutine;
        set
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = value;
        }
    }

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
        => Coroutine = StartCoroutine(FillAmountImageCoroutine(_hpbar, hp, maxHp));

    private IEnumerator FillAmountImageCoroutine(Image target, int currentValue, int maxValue)
    {
        float end = (float)currentValue / maxValue;

        while (!Mathf.Approximately(target.fillAmount, end))
        {
            target.fillAmount = Mathf.MoveTowards(target.fillAmount, end, 1f * Time.deltaTime);
            yield return null;
        }

        target.fillAmount = end;

        StopCoroutine(Coroutine);
    }
}