using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("Import")]
    [SerializeField] Transform _canvas;
    [SerializeField] InventoryManager _inventoryManager;

    [Header("SlotInfo")]
    [SerializeField] SlotType _type;
    [SerializeField] Item _item;
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
            SetSlotUI();
        }
    }
    public Image Image => _image;

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

    /*Move*/
    private Vector3 originalPosition;
    private float scaleFactor;



    private void Start()
    {
        scaleFactor = _canvas.GetComponent<CanvasScaler>().scaleFactor;
    }

    private void Reset()
    {
        _inventoryManager = GetComponentInParent<InventoryManager>();
        _canvas = GetComponentInParent<InventoryManager>().transform;
        _image = GetComponentsInChildren<Image>()[^1];
        transform.parent.GetChild(1).gameObject.SetActive(true);
        _countText = GetComponentInChildren<TextMeshProUGUI>();
        _countText.gameObject.SetActive(false);
    }



    /*Logic*/
    //=======================================================//


    private void SetSlotUI()
    {
        if (_item == null)
        {
            _image.sprite = null;
        }
        else
        {
            if (Item is SingleUseItem)
                _countText.gameObject.SetActive(true);
            _image.sprite = Item.Icon;
        }
    }


    /*ButtonEvent*/
    //=======================================================//

    /// <summary>
    /// 마우스 버튼 또는 터치 눌림 순간 감지
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    /// <summary>
    /// 마우스 버튼 또는 터치 떼는 순간 감지
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
    }

    /// <summary>
    /// 마우스 커서가 버튼 위로 올라갈 때 감지
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    /// <summary>
    /// 마우스 커서가 버튼 밖으로 나갈 때 감지
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
    }

    /// <summary>
    /// 드래그 시작 시 호출
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null) return;
        originalPosition = Image.rectTransform.anchoredPosition;
        Image.transform.SetParent(_canvas, true);
    }

    /// <summary>
    /// 드래그 중 계속 호출
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (Item == null) return;
        Image.rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    /// <summary>
    /// 드래그 종료 시 호출
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Image.transform.parent == transform) return;
        Image.transform.SetParent(transform, true);
        Image.transform.SetAsFirstSibling();
        Image.rectTransform.anchoredPosition = originalPosition;
    }

    /// <summary>
    /// 드래그한 객체가 해당 버튼 위에 놓였을 때 호출
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        var droppedItem = eventData.pointerDrag;
        if (droppedItem == null) return;

        if (droppedItem.TryGetComponent<SlotUI>(out var droppedSlot))
        {
            _inventoryManager.SwapItem(droppedSlot, this);
        }
    }
}
