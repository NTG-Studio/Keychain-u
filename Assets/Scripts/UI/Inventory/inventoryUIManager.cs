using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Items;

namespace UI.Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        [SerializeField] private List<int> displayItems;
        [SerializeField] private bool playerInventoryEmpty;
        [SerializeField] private bool inventoryVisible;
        
        public List<InventorySlot> slots;

        public InventoryUISelection currentSelection = InventoryUISelection.ScrollList;

        [Expandable] public Item currentItem;
        
        [Header("References")]
     
        public UINode useEquip;
        public UINode combine;
        public UINode discard;
        
        [BoxGroup("Inventory Management")] [SerializeField] private PlayerInventory pInventory;
        [BoxGroup("Inventory Management")] [SerializeField] private Animator inventoryAnim;


        //private helper parameters
        private bool _lastOperationIsDown = true;
        private static readonly Color DarkColor = new Color(0.2f, 0.2f, 0.2f, 1);
        private static readonly Color LightColor = new Color(0.8f, 0.8f, 0.8f, 1);

        // Start is called before the first frame update
        void Start()
        {
            RefreshAll();
        }

        // Update is called once per frame
        private void Update()
        {
            //set the inventory empty tag
            playerInventoryEmpty = (pInventory.GetCount() == 0);
            inventoryAnim.SetBool("inventoryVisible", inventoryVisible);

            //adjust the current selection
            if (_lastOperationIsDown)
            {
                UpdateSelection();
            }
            else
            {
                UpdateSelectionUpward();
            }
        }

        /// <summary>
        /// makes sure all slots stay in the range they should be which should be between 0 and the player inventory count
        /// </summary>
        private void ProcessSlots()
        {
            if (!playerInventoryEmpty)
            {
                for (int i = 0; i < displayItems.Count; i++)
                {
                    while (!SlotInRange(i))
                    {
                        ProcessSlot(i);
                    }
                }
            }
        }

        /// <summary>
        /// makes sure a single slot stays in the range it should be
        /// </summary>
        private void ProcessSlot(int index)
        {
            if (pInventory.GetCount() == 1)
            {
                displayItems[index] = 0;
            }
            else
            {
                if (displayItems[index] < 0)
                {
                    displayItems[index] = pInventory.GetCount() - 1;
                }

                if (displayItems[index] >= pInventory.GetCount())
                {
                    displayItems[index] = 0;
                }
            }
        }

        /// <summary>
        /// returns true if the slot index is in range, false if now
        /// </summary>
        /// <param name="index">the slot to check</param>
        /// <returns>is the slot in range or nah?</returns>
        private bool SlotInRange(int index)
        {
            if (displayItems[index] >= 0 && displayItems[index] < pInventory.GetCount())
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
        void MoveRight()
        {
            if (!playerInventoryEmpty)
            {
                for (int i = 0; i < displayItems.Count; i++)
                {
                    displayItems[i]--;
                }

                ProcessSlots();
                if (slots.Count is not 0)
                {
                    foreach (var s in slots)
                    {
                        s.MoveLeft();
                    }
                }
            }
        }

        /// <summary>
        /// moves all slots one to the right
        /// </summary>
        void MoveLeft()
        {
            if (!playerInventoryEmpty)
            {
                for (int i = 0; i < displayItems.Count; i++)
                {
                    displayItems[i]++;
                }

                ProcessSlots();
                if (slots.Count is not 0)
                {
                    foreach (var s in slots)
                    {
                        s.MoveRight();
                    }
                }
            }
        }

        /// <summary>
        /// gets a value out of slots
        /// </summary>
        /// <param name="slot">the index to pull from</param>
        /// <returns>the item index</returns>
        public int GetIndex(int slot)
        {
            return displayItems[slot];
        }

        /// <summary>
        /// reset the bulk of the data
        /// </summary>
        public void RefreshAll()
        {
            /*Chat gpt things these numbers are arbitrary... they arent lol*/
            displayItems = new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                7
            };

            //if player inventory is null for whatever reason (scene switching etc) then refresh the reference.
            pInventory = pInventory ? pInventory : GameObject.FindObjectOfType<PlayerInventory>();
            
            //set the player inventory
            playerInventoryEmpty = (pInventory == null || pInventory.GetCount() == 0);
            
            //update all of the slots 
            ProcessSlots();

            //update each individual slot object
            slots.ForEach(s => s.Refresh());
        }

        /// <summary>
        /// update the current selection moving downward
        /// </summary>
        private void UpdateSelection()
        {
            //if the item is valid
            if (currentItem == null) return;
                if (currentSelection == 0) return;
                    switch (currentSelection)
                    {
                        case InventoryUISelection.UseEquip:
                            UpdateButtons(true, false, false);
                            if (!currentItem.equippable && !currentItem.usable)
                            {
                                currentSelection++;
                            }

                            break;
                        case InventoryUISelection.Discard:
                            UpdateButtons(false, false, true);
                            if (!currentItem.discardable)
                            {
                                currentSelection++;
                            }

                            break;
                        case InventoryUISelection.Combine:
                            UpdateButtons(false, true, false);
                            if (!currentItem.combineable)
                            {
                                currentSelection++;
                            }

                            break;
                        case InventoryUISelection.Def:
                            currentSelection = InventoryUISelection.ScrollList;
                            UpdateButtons(false, false, false);
                            break;
                        default:
                            currentSelection = InventoryUISelection.ScrollList;
                            UpdateButtons(false, false, false);
                            break;
                    }
        }

        /// <summary>
        /// update the current selection, moving upward if the value is invalid
        /// </summary>
        private void UpdateSelectionUpward()
        {
            //if the item is valid
            if (currentItem != null)
            {
                if (currentSelection != 0)
                {
                    switch (currentSelection)
                    {
                        case InventoryUISelection.UseEquip:
                            UpdateButtons(true, false, false);
                            if (!currentItem.equippable && !currentItem.usable)
                            {
                                currentSelection--;
                            }

                            break;
                        case InventoryUISelection.Discard:
                            UpdateButtons(false, false, true);
                            if (!currentItem.discardable)
                            {
                                currentSelection--;
                            }

                            break;
                        case InventoryUISelection.Combine:
                            UpdateButtons(false, true, false);
                            if (!currentItem.combineable)
                            {
                                currentSelection--;
                            }

                            break;
                        case InventoryUISelection.Def:
                            currentSelection = InventoryUISelection.ScrollList;
                            UpdateButtons(false, false, false);
                            break;
                        default:
                            currentSelection = InventoryUISelection.ScrollList;
                            UpdateButtons(false, false, false);
                            break;
                    }
                }
            }


        }

        /// <summary>
        /// update the buttons visibility for current selection
        /// </summary>
        /// <param name="equipUse"></param>
        /// <param name="comb"></param>
        /// <param name="disc"></param>
        private void UpdateButtons(bool equipUse, bool comb, bool disc)
        {
            AdjustButtonColors(equipUse,useEquip);
            AdjustButtonColors(comb,combine);
            AdjustButtonColors(disc,discard);
        }

        /// <summary>
        /// adjusts the colors for a single button/text pair
        /// </summary>
        /// <param name="flag">whether that particular thing is enabled</param>
        /// <param name="n">the button to adjust</param>
        private void AdjustButtonColors(bool flag, UINode n)
        {
            if (flag)
            {
                n.SetColors(LightColor,DarkColor);
            }
            else
            {
                n.SetColors(DarkColor,LightColor);
            }
        }

        /// <summary>
        /// move the current selection down
        /// </summary>
        public void ScrollDown()
        {
            if (inventoryVisible)
            {
                currentSelection++;
                UpdateSelection();
            }
        }

        /// <summary>
        /// move the current selection up
        /// </summary>
        public void ScrollUp()
        {
            if (inventoryVisible)
            {
                currentSelection--;
                UpdateSelectionUpward();
            }
        }

        /// <summary>
        /// scroll right
        /// </summary>
        public void ScrollInventoryRight()
        {
            if (currentSelection == InventoryUISelection.ScrollList && inventoryVisible)
            {
                MoveRight();
            }
        }

        /// <summary>
        /// scroll left
        /// </summary>
        public void ScrollInventoryLeft()
        {
            if (currentSelection == InventoryUISelection.ScrollList && inventoryVisible)
            {
                MoveLeft();
            }
        }

        /// <summary>
        /// do the inventory action as determined by what is being pressed
        /// </summary>
        public void InventoryAction()
        {

            switch (currentSelection)
            {
                case InventoryUISelection.UseEquip:
                    if (currentItem.equippable)
                    {
                        //item is equitable
                        pInventory.equippedIndex = pInventory.FindItemIndex(currentItem);
                    }
                    else
                    {
                        if (currentItem.usable)
                        {
                            //item is usable
                        }
                    }

                    break;
                case InventoryUISelection.Discard:
                    pInventory.RemoveItem(currentItem);
                    RefreshAll();
                    break;
                
                case InventoryUISelection.Combine:
                    break;
                
                case InventoryUISelection.ScrollList:
                    currentSelection = InventoryUISelection.UseEquip;
                    UpdateSelection();
                    break;
            }

        }

        /// <summary>
        /// responds the the Inventory Button Event
        /// </summary>
        public void OpenCloseInventory()
        {
            Debug.Log("inventory");
            inventoryVisible = !inventoryVisible;
            if (inventoryVisible)
            {
                currentSelection = 0;
            }
        }
    }

    public enum InventoryUISelection
    {
        ScrollList = 0,
        UseEquip = 1,
        Combine = 2,
        Discard = 3,
        Def = 4
    }
}

