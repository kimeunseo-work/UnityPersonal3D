public class StaminaUI : StatUI
{
    protected override void OnEnable()
    {
        _owner.OnStaminaChanged += HandleStatChanged;
    }
    protected override void OnDisable()
    {
        _owner.OnStaminaChanged -= HandleStatChanged;
    }
}