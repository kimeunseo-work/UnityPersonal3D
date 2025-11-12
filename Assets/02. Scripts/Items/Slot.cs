using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Item _item;
    [SerializeField] Image _image;
    [SerializeField] Button _btn;
    [SerializeField] TextMeshProUGUI _countText;

    public int _currentStackCount = 0;
    public int CurrentStackCount
    {
        get => _currentStackCount;
        set
        {
            _currentStackCount = value;
            _countText.text = value.ToString();
        }
    }
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            SetSlotUI();
        }
    }



    private void Start()
    {
        _countText.gameObject.SetActive(false);
        _btn.interactable = false;
    }



    private void SetSlotUI()
    {
        if (_item == null)
        {
            _image.sprite = null;
            _btn.interactable = false;
        }
        else
        {
            if (Item is SingleUseItem)
                _countText.gameObject.SetActive(true);
            _image.sprite = Item.Icon;
            _btn.interactable = true;
        }
    }
}
