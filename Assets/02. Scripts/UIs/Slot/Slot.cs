using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("Import")]
    [SerializeField] Transform _canvas;
    [SerializeField] InventoryManager _manager;

    [Header("Slot")]
    [SerializeField] SlotUI _ui;
    [SerializeField] SlotInput _input;
    [SerializeField] Item _item;

    [Header("SlotInfo")]
    [SerializeField] SlotType _type;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _countText;

    /*SerializeField Properties*/
    public SlotType Type => _type;
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            _ui.SetSlotUI();
        }
    }
    public Image Image => _image;
    public TextMeshProUGUI CountText => _countText;

    /*For SingleUse*/
    private int _currentStackCount = 0;
    public int CurrentStackCount
    {
        get => _currentStackCount;
        set
        {
            _currentStackCount = value;
            _countText.text = value.ToString();
        }
    }



    private void Reset()
    {
        _canvas = GetComponentInParent<Canvas>().transform;
        _manager = GetComponentInParent<InventoryManager>();

        _ui = GetComponent<SlotUI>();
        _input = GetComponent<SlotInput>();

        _image = GetComponentsInChildren<Image>()[^1];
        transform.GetChild(1).gameObject.SetActive(true);
        _countText = GetComponentInChildren<TextMeshProUGUI>();
        _countText.gameObject.SetActive(false);
    }
    private void Start()
    {
        _ui.Init(this);
        _input.Init(this, _canvas);
    }



    public void HandleSlotDoubleClick()
    {
        if (_item is ReusableItem item)
        {
            _manager.HandleSlotDoubleClick(item);
            return;
        }
    }


    public void BeingDrag(ref Vector3 originalPosition)
    {
        if (Item == null) return;
        originalPosition = Image.rectTransform.anchoredPosition;
        Image.transform.SetParent(_canvas, true);
    }
    public void Drag(Vector2 delta, float scaleFactor)
    {
        if (Item == null) return;
        Image.rectTransform.anchoredPosition += delta / scaleFactor;
    }
    public void EndDrag(Vector3 originalPosition)
    {
        if (Image.transform.parent == transform) return;
        Image.transform.SetParent(transform, true);
        Image.transform.SetAsFirstSibling();
        Image.rectTransform.anchoredPosition = originalPosition;
    }

    public void OnDrop(GameObject droppedItem)
    {
        if (droppedItem == null) return;

        if (droppedItem.TryGetComponent<Slot>(out var droppedSlot))
        {
            _manager.SwapItem(droppedSlot, this);
        }
    }
}