using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public List<PlayerInventorySlot> inventory;

    public int[] slots = new int[9];

    private void Start()
    {
        UpdateSlots();
    }

    /// <summary>
    /// set or rest the inventory slots
    /// </summary>
    public void UpdateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //repeat all the slots until all of the slots are full
            slots[i] = i % inventory.Count;
        }
    }
    
    /// <summary>
    /// shit the inventory up or down
    /// </summary>
    /// <param name="shiftUp"></param>
    // Function to shift the inventory slots up or down
    public void ShiftInventory(bool shiftUp)
    {
        // If there are no items, set all slots to -1
        if (inventory.Count == 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = -1;
            }
        }
        // If there's only one item, fill all slots with that item
        else if (inventory.Count == 1)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = 0;
            }
        }
        else
        {
            int shiftAmount = shiftUp ? 1 : -1;
            for (int i = 0; i < slots.Length; i++)
            {
                // Update each slot with the new index, making sure to keep it within the bounds of the item list
                slots[i] = (slots[i] + shiftAmount + inventory.Count) % inventory.Count;
            }
        }
    }

    [Button]
    public void shiftUp()
    {
        ShiftInventory(true);
    }
    
    [Button]
    public void shiftDown()
    {
        ShiftInventory(false);
    }
}
