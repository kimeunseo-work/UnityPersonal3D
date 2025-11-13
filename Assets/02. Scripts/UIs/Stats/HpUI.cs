public class HpUI : StatUI
{
    protected override void OnEnable()
    {
        _owner.OnHpChanged += HandleStatChanged;
    }
    protected override void OnDisable()
    {
        _owner.OnHpChanged -= HandleStatChanged;
    }
}