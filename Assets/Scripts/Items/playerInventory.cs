using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// a container class for the players inventory, keeps track of adding and removing items
/// as well as finding items and managinging what item is equipped
/// this class however contains NO information on actually using items
/// </summary>
public class playerInventory : MonoBehaviour
{
    /// <summary>
    /// the players full inventory
    /// </summary>
    [SerializeField] private List<itemSlot> slots;

    public int count = 0;
    public item testItem;
    /// <summary>
    /// the item index of the currently equipped item, -1 means no item is equipped
    /// </summary>
    [SerializeField] private int equipped_index = -1;


    private void Start()
    {
        //create the item slots
        if (slots == null)
        {
            slots = new List<itemSlot>();
        }
    }

    private void Update()
    {
        count = getCount();
    }

    /// <summary>
    /// find a specific item by reference 
    /// </summary>
    /// <param name="itm">the item to find</param>
    /// <returns>returns the item slot</returns>
    public itemSlot findItem(item itm)
    {
        for (int i=0;i<slots.Count;i++)
        {
            if (slots[i].itm == itm)
            {
                return slots[i];
            }
        }

        return null;
    }
    
    /// <summary>
    /// Finds a given item and returns the index it is stored at
    /// </summary>
    /// <param name="itm">the item to search for</param>
    /// <returns>returns the index if the item was found, otherwise returns -1</returns>
    public int findItemIndex(item itm)
    {
        for (int i=0;i<slots.Count;i++)
        {
            if (slots[i].itm == itm)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// adds an item to the list (intelligently)
    /// </summary>
    /// <param name="itm">the item to be added</param>
    /// <returns>return true if it succeeds and false if it fails</returns>
    public bool addItem(item itm)
    {
        int index = findItemIndex(itm);
        if (index == -1)
        {
            //the item does not already exist
            addNewItem(itm);
            return true;
        }
        else
        {
            if (slots[index].itm.stackable)
            {
                //item is stackable
                if (slots[index].stack < 20)
                {
                    //the item has room to add to the stack
                    slots[index].stack++;
                    return true;
                }
                else
                {
                    //there is not additional room in the stack, add a new item
                    addNewItem(itm);
                    return true;
                    //TODO: add additional flag for if the player should be able to add another stack or not
                }
            }
            else
            {
                //as of right now this should not be reachable
                return false;
            }
        }
    }

    /// <summary>
    /// removes an item from the list (for example if it is used or throw away
    /// </summary>
    /// <param name="itm">the item to be removed</param>
    /// <returns>true if the item was successfully removed (it exists) false if not</returns>
    public bool removeItem(item itm)
    {
        int index = findItemIndex(itm);
        if (index == -1)
        {
            //was not able to find the item
            return false;
        }
        else
        {
            //the item was found
            if (slots[index].itm.stackable)
            {
                //the item is stackable
                if (slots[index].stack > 1)
                {
                    //reduce the stack by 1
                    slots[index].stack--;
                    return true;
                }
                else
                {
                    //remove the whole slot
                    slots.RemoveAt(index);
                    return true;
                }
            }
            else
            {
                //item is not stackable so remove the whole thing
                slots.RemoveAt(index);
                return false;
            }
        }
    }

    /// <summary>
    /// forces an item to be added to the end of the list
    /// </summary>
    /// <param name="itm">the item to be forced</param>
    private void addNewItem(item itm)
    {
        itemSlot s = new itemSlot();
        s.itm = itm;
        s.stack = 1;
        slots.Add(s);
    }

    /// <summary>
    /// just gets the number of items in the inventory
    /// </summary>
    /// <returns>the count of items</returns>
    public int getCount()
    {
        return slots.Count;
    }

    [Button]
    public void addTestItem()
    {
        addNewItem(testItem);
    }

    /// <summary>
    /// gets the item at "index" and returns it
    /// </summary>
    /// <param name="index">the index to pull from</param>
    /// <returns>an item slot</returns>
    public itemSlot getItemByIndex(int index)
    {
        return slots[index];
    }
}
