using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("InventoryArea")]
    [SerializeField] Transform ArmorArea;
    [SerializeField] Transform WeaponArea;
    [SerializeField] Transform ActiveItemArea;
    [SerializeField] Transform ItemArea;

    public Item Armor { get; private set; }
    public Slot ArmorSlot { get; private set; }
    public Item Weapon { get; private set; }
    public Slot WeaponSlot { get; private set; }
    public List<Item> ActiveItems { get; private set; } = new(4);
    public List<Slot> ActiveItemSlots { get; private set; }
    public List<Item> Items { get; private set; } = new(28);
    public List<Slot> Slots { get; private set; }



    private void Start()
    {
        ArmorSlot = ArmorArea.GetComponentInChildren<Slot>();
        WeaponSlot = WeaponArea.GetComponentInChildren<Slot>();
        ActiveItemSlots = ActiveItemArea.GetComponentsInChildren<Slot>().ToList();
        Slots = ItemArea.GetComponentsInChildren<Slot>().ToList();
    }



    public void GetItem(Item targetItem)
    {
        if (Items.Contains(targetItem))
        {
            var index = Items.IndexOf(targetItem);
            var slot = Slots[index];

            if (Slots[index].Item is SingleUseItem)
                slot.CurrentStackCount++;
            else
                Debug.Log($"[{gameObject.name}] Item is not SingleUse");
        }
        else
        {
            Items.Add(targetItem);
            var index = Items.IndexOf(targetItem);
            Slots[index].Item = targetItem;
        }
    }

    public void RemoveItem(Item item)
    {
        var index = Items.IndexOf(item);
        Items.RemoveAt(index);
        Slots[index].Item = null;
    }
}