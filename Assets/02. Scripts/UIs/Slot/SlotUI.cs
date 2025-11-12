using UnityEngine;

public class SlotUI : MonoBehaviour
{
    private Slot _slot;
    public void Init(Slot slot)
    {
        _slot = slot;
    }

    public void SetSlotUI()
    {
        if (_slot.Item == null)
        {
            _slot.Image.sprite = null;
        }
        else
        {
            if (_slot.Item is SingleUseItem)
                _slot.CountText.gameObject.SetActive(true);
            _slot.Image.sprite = _slot.Item.Icon;
        }
    }
}