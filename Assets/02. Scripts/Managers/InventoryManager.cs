using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("SlotType")]
    [SerializeField] SlotType ArmorType;
    [SerializeField] SlotType WeaponType;
    [SerializeField] SlotType ActiveItemType;
    [SerializeField] SlotType ItemType;

    [Header("InventoryArea")]
    [SerializeField] Transform ArmorArea;
    [SerializeField] Transform WeaponArea;
    [SerializeField] Transform ActiveItemArea;
    [SerializeField] Transform ItemArea;

    public SlotUI ArmorSlot { get; private set; }
    public SlotUI WeaponSlot { get; private set; }
    public List<SlotUI> ActiveItemSlots { get; private set; }
    public List<SlotUI> Slots { get; private set; }

    public Dictionary<SlotType, List<SlotUI>> SlotDictionary;

    private Dictionary<string, Item> _itemCache = new(28);


    private void Start()
    {
        ArmorSlot = ArmorArea.GetComponentInChildren<SlotUI>();
        WeaponSlot = WeaponArea.GetComponentInChildren<SlotUI>();
        ActiveItemSlots = ActiveItemArea.GetComponentsInChildren<SlotUI>().ToList();
        Slots = ItemArea.GetComponentsInChildren<SlotUI>().ToList();

        SlotDictionary = new Dictionary<SlotType, List<SlotUI>>(4)
        {
            { ArmorType, new List<SlotUI> { ArmorSlot } },
            { WeaponType, new List<SlotUI> { WeaponSlot } },
            { ActiveItemType, ActiveItemSlots },
            { ItemType, Slots },
        };
    }



    public void GetItem(Item targetItem)
    {
        if (_itemCache.ContainsKey(targetItem.Name))
        {
            var slot = Slots.Find(x => x.Item == targetItem);

            if (slot.Item is SingleUseItem)
                slot.CurrentStackCount++;
            else
                Debug.Log($"[{gameObject.name}] Item is not SingleUse");
        }
        else
        {
            Slots
                .Find(x => x.Item == null)
                .Item = targetItem;

            _itemCache.Add(targetItem.Name, targetItem);
        }
    }

    public void SwapItem(SlotUI droppedSlot, SlotUI targetSlot)
    {
        var droppedItem = droppedSlot.Item;
        var targetItem = targetSlot.Item;

        var droppedIndex = SlotDictionary[droppedSlot.Type].IndexOf(droppedSlot);
        var targetIndex = SlotDictionary[targetSlot.Type].IndexOf(targetSlot);

        SlotDictionary[droppedSlot.Type][droppedIndex].Item = targetItem;
        SlotDictionary[targetSlot.Type][targetIndex].Item = droppedItem;
    }
}