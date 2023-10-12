using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UI.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    /// <summary>
    /// a container class for the players inventory, keeps track of adding and removing items
    /// as well as finding items and managinging what item is equipped
    /// this class however contains NO information on actually using items
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        /// <summary>
        /// the players full inventory
        /// </summary>
        [SerializeField] private List<ItemSlot> _slots;

        public int count;
        public Item[] testItem;
        /// <summary>
        /// the item index of the currently equipped item, -1 means no item is equipped
        /// </summary>
        [FormerlySerializedAs("equipped_index")] [SerializeField] public int equippedIndex = -1;

        /// <summary>
        /// a reference to the ui manager so we can refresh everything
        /// </summary>
        [SerializeField] private InventoryUIManager manager;


        private void Start()
        {
            //create the item slots
            _slots ??= new List<ItemSlot>();
        }

        private void Update()
        {
            count = GetCount();
        }

        /// <summary>
        /// find a specific item by reference 
        /// </summary>
        /// <param name="itm">the item to find</param>
        /// <returns>returns the item slot</returns>
        public ItemSlot FindItem(Item itm)
        {
            return _slots.FirstOrDefault(t => t.Itm == itm);
        }
    
        /// <summary>
        /// Finds a given item and returns the index it is stored at
        /// </summary>
        /// <param name="itm">the item to search for</param>
        /// <returns>returns the index if the item was found, otherwise returns -1</returns>
        public int FindItemIndex(Object itm)
        {
            for (var i=0;i<_slots.Count;i++)
            {
                if (_slots[i].Itm == itm)
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
        public bool AddItem(Item itm)
        {
            int index = FindItemIndex(itm);
            if (index == -1)
            {
                //the item does not already exist
                AddNewItem(itm);
                return true;
            }
            else
            {
                if (_slots[index].Itm.stackable)
                {
                    //item is stackable
                    if (_slots[index].Stack < 20)
                    {
                        //the item has room to add to the stack
                        _slots[index].Stack++;
                        return true;
                    }
                    else
                    {
                        //there is not additional room in the stack, add a new item
                        AddNewItem(itm);
                        if (manager == null)
                        {
                            manager = GameObject.FindObjectOfType<InventoryUIManager>();
                        }

                        if (manager != null)
                        {
                            manager.RefreshAll();
                        }
                        return true;
                        //TODO: add additional flag for if the player should be able to add another stack or not
                    }
                }
                else
                {
                    //as of right now this should not be reachable
                    if (manager == null)
                    {
                        manager = GameObject.FindObjectOfType<InventoryUIManager>();
                    }

                    if (manager != null)
                    {
                        manager.RefreshAll();
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// removes an item from the list (for example if it is used or throw away
        /// </summary>
        /// <param name="itm">the item to be removed</param>
        /// <returns>true if the item was successfully removed (it exists) false if not</returns>
        public bool RemoveItem(Item itm)
        {
            int index = FindItemIndex(itm);
            if (index == -1)
            {
                //was not able to find the item
                if (manager == null)
                {
                    manager = GameObject.FindObjectOfType<InventoryUIManager>();
                }

                if (manager != null)
                {
                    manager.RefreshAll();
                }
                return false;
            }
            else
            {
                //the item was found
                if (_slots[index].Itm.stackable)
                {
                    //the item is stackable
                    if (_slots[index].Stack > 1)
                    {
                        //reduce the stack by 1
                        _slots[index].Stack--;
                        return true;
                    }
                    else
                    {
                        //remove the whole slot
                        _slots.RemoveAt(index);
                        return true;
                    }
                }
                else
                {
                    //item is not stackable so remove the whole thing
                    _slots.RemoveAt(index);
                    if (manager == null)
                    {
                        manager = GameObject.FindObjectOfType<InventoryUIManager>();
                    }

                    if (manager != null)
                    {
                        manager.RefreshAll();
                    }
                    return true;
                }
            }
        }

        /// <summary>
        /// forces an item to be added to the end of the list
        /// </summary>
        /// <param name="itm">the item to be forced</param>
        private void AddNewItem(Item itm)
        {
            var s = new ItemSlot
            {
                Itm = itm,
                Stack = 1
            };
            _slots.Add(s);
            if (manager == null)
            {
                manager = GameObject.FindObjectOfType<InventoryUIManager>();
            }

            if (manager != null)
            {
                manager.RefreshAll();
            }
        }

        /// <summary>
        /// just gets the number of items in the inventory
        /// </summary>
        /// <returns>the count of items</returns>
        public int GetCount()
        {
            return _slots?.Count ?? 0;
        }

        [Button]
        public void AddTestItem()
        {
            foreach (Item i in testItem)
            {
                AddNewItem(i);
            }
        }

        /// <summary>
        /// gets the item at "index" and returns it
        /// </summary>
        /// <param name="index">the index to pull from</param>
        /// <returns>an item slot</returns>
        public ItemSlot GetItemByIndex(int index)
        {
            return _slots[index];
        }
    }
}
