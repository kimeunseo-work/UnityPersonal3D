using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    /*Move*/
    private Slot _slot;
    private Vector3 _originalPosition;
    private float _scaleFactor;

    /*doubleClick*/
    private float lastClickTime;
    private const float doubleClickThreshold = 0.25f; // 초 단위



    public void Init(Slot slot, Transform canvas)
    {
        _slot = slot;
        _scaleFactor = canvas
            .GetComponent<CanvasScaler>()
            .scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_slot.Item == null) return;
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            Debug.Log($"[{gameObject.name}] doubleClick");
            _slot.HandleSlotDoubleClick();
            lastClickTime = 0;
        }
        else
        {
            Debug.Log($"[{gameObject.name}] not doubleClick");
            lastClickTime = Time.time;
        }
    }

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
        => _slot.BeingDrag(ref _originalPosition);

    /// <summary>
    /// 드래그 중 계속 호출
    /// </summary>
    public void OnDrag(PointerEventData eventData)
        => _slot.Drag(eventData.delta, _scaleFactor);

    /// <summary>
    /// 드래그 종료 시 호출
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
        => _slot.EndDrag(_originalPosition);

    /// <summary>
    /// 드래그한 객체가 해당 버튼 위에 놓였을 때 호출
    /// </summary>
    public void OnDrop(PointerEventData eventData)
        => _slot.OnDrop(eventData.pointerDrag);
}