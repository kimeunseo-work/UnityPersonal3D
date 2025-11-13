using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatUI : MonoBehaviour
{
    [SerializeField] protected Player _owner;

    [Header("Stat")]
    [SerializeField] protected Image _statbar;

    protected Coroutine _coroutine;
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

    protected void Reset()
    {
        _owner = GetComponentInParent<Player>();
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();

    protected void HandleStatChanged(int value, int maxValue)
        => Coroutine = StartCoroutine(FillAmountImageCoroutine(_statbar, value, maxValue));

    protected IEnumerator FillAmountImageCoroutine(Image target, int currentValue, int maxValue)
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
