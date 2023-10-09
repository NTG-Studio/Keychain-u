using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class inventoryUIManager : MonoBehaviour
{
    [SerializeField] private List<int> displayItems;

    [SerializeField] private playerInventory p_inventory;

    [SerializeField] private bool playerInventoryEmpty = false;
    // Start is called before the first frame update
    void Start()
    {
        displayItems = new List<int>();
        displayItems.Add(0);
        displayItems.Add(1);
        displayItems.Add(2);
        displayItems.Add(3);
        displayItems.Add(4);
        displayItems.Add(5);
        displayItems.Add(7);
        
        if (p_inventory == null)
        {
            p_inventory = GameObject.FindObjectOfType<playerInventory>();
        }
        
        if (p_inventory.getCount() == 0)
        {
            playerInventoryEmpty = true;
        }
        else
        {
            playerInventoryEmpty = false;
        }
        
        processSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (p_inventory.getCount() == 0)
        {
            playerInventoryEmpty = true;
        }
        else
        {
            playerInventoryEmpty = false;
        }
    }

    /// <summary>
    /// makes sure all slots stay in the range they should be
    /// </summary>
    private void processSlots()
    {
        if (!playerInventoryEmpty)
        {
            for (int i = 0; i < displayItems.Count; i++)
            {
                while (!slotInRange(i))
                {
                    processSlot(i);
                }
            }
        }
    }

    /// <summary>
    /// makes sure a single slot stays in the range it should be
    /// </summary>
    private void processSlot(int index)
    {
        if (displayItems[index] < 0)
        {
            displayItems[index] = p_inventory.getCount() - 1;
        }

        if (displayItems[index] >= p_inventory.getCount())
        {
            displayItems[index] = 0;
        }
    }

    /// <summary>
    /// returns true if the slot index is in range, false if now
    /// </summary>
    /// <param name="index">the slot to check</param>
    /// <returns>is the slot in range or nah?</returns>
    private bool slotInRange(int index)
    {
        if (displayItems[index] > 0 && displayItems[index] < p_inventory.getCount())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// moves all slots one to the left
    /// </summary>
    [Button]
    public void moveLeft()
    {
        if (!playerInventoryEmpty)
        {
            for (int i = 0; i < displayItems.Count; i++)
            {
                displayItems[i]--;
            }
            processSlots();
        }
    }

    /// <summary>
    /// moves all slots one to the right
    /// </summary>
    [Button]
    public void moveRight()
    {
        if (!playerInventoryEmpty)
        {
            for (int i = 0; i < displayItems.Count; i++)
            {
                displayItems[i]++;
            }
            processSlots();
        }
    }
}
