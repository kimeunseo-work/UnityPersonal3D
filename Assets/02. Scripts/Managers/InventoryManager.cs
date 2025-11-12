using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Player _player;

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

    public Slot ArmorSlot { get; private set; }
    public Slot WeaponSlot { get; private set; }
    public List<Slot> ActiveItemSlots { get; private set; }
    public List<Slot> Slots { get; private set; }

    public Dictionary<SlotType, List<Slot>> SlotDictionary;

    private Dictionary<string, Item> _itemCache = new(4);

    public Player Player => _player;


    private void Start()
    {
        ArmorSlot = ArmorArea.GetComponentInChildren<Slot>();
        WeaponSlot = WeaponArea.GetComponentInChildren<Slot>();
        ActiveItemSlots = ActiveItemArea.GetComponentsInChildren<Slot>().ToList();
        Slots = ItemArea.GetComponentsInChildren<Slot>().ToList();

        SlotDictionary = new Dictionary<SlotType, List<Slot>>(4)
        {
            { ArmorType, new List<Slot> { ArmorSlot } },
            { WeaponType, new List<Slot> { WeaponSlot } },
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

    public void SwapItem(Slot droppedSlot, Slot targetSlot)
    {
        var droppedItem = droppedSlot.Item;
        var targetItem = targetSlot.Item;

        var droppedIndex = SlotDictionary[droppedSlot.Type].IndexOf(droppedSlot);
        var targetIndex = SlotDictionary[targetSlot.Type].IndexOf(targetSlot);

        SlotDictionary[droppedSlot.Type][droppedIndex].Item = targetItem;
        SlotDictionary[targetSlot.Type][targetIndex].Item = droppedItem;
    }

    public void HandleSlotDoubleClick(ReusableItem item)
        => _player.UseItem(item.Activate());
}