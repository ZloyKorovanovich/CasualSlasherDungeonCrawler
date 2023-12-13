using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly List<InventorySlot> _inventorySlots = new();

    private ItemData _itemInHand;

    public ItemData ItemInHand
    {
        get => _itemInHand;
        set
        {
            var tempItem = value;

            if (_itemInHand != null)
            {
                OnDropedWeapon?.Invoke(_itemInHand);
                Debug.Log("Weapon is changed!");
            }

            _itemInHand = tempItem;
            Debug.Log("Weapon is taked!");
        }
    }

    public event Action<ItemData> OnDropedWeapon;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var slot = new InventorySlot();
            _inventorySlots.Add(slot);
        }
        Debug.Log(_inventorySlots.Count);
    }

    public void AddItem(ItemData item)
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            var slot = _inventorySlots[i];

            if (slot.ItemData != null) continue;

            slot.ItemData = item;

            Debug.Log("Item is taked in slot: " + i);

            if (item.ItemType == ItemType.Weapon)
            {
                ItemInHand = item;
            }

            break;
        }
    }

    public void RemoveItem(int index)
    {
        _inventorySlots.RemoveAt(index);
        Debug.Log("Item is removed in slot: " + index);
    }
}
