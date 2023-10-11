using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nova;
using UnityEngine;
using UnityEngine.Events;

public class inventoryUIManager : MonoBehaviour
{
    
    [BoxGroup("References")] [SerializeField] private List<int> displayItems;
    [BoxGroup("References")] [SerializeField] private playerInventory p_inventory;
    [SerializeField] private bool playerInventoryEmpty = false;
    [SerializeField] private bool inventoryVisible = false;
    [BoxGroup("References")] [SerializeField] private Animator InventoryAnim ;

    [HorizontalLine] private int spacer = 0;
    [BoxGroup("Events")][SerializeField] public UnityEvent scrollLeft;
    [BoxGroup("Events")][SerializeField] public UnityEvent scrollRight;
    [BoxGroup("Events")][SerializeField] public UnityEvent refresh;

    public InventoryUISelection currentSelection = 0;

    public item CurrentItem;
    
    [BoxGroup("Buttons")] [SerializeField] private UIBlock2D useEquipButton;
    [BoxGroup("Buttons")] [SerializeField] private TextBlock useEquipText;
    [BoxGroup("Buttons")] [SerializeField] private UIBlock2D combineButton;
    [BoxGroup("Buttons")] [SerializeField] private TextBlock combineText;
    [BoxGroup("Buttons")] [SerializeField] private UIBlock2D discardButton;
    [BoxGroup("Buttons")] [SerializeField] private TextBlock DiscardText;

    private bool lastOperationisDown = true;
    // Start is called before the first frame update
    void Start()
    {
       RefreshAll();
    }

    // Update is called once per frame
    void Update()
    {
        //set the inventory empty tag
        if (p_inventory.getCount() == 0)
        {
            playerInventoryEmpty = true;
        }
        else
        {
            playerInventoryEmpty = false;
        }

        //open and close the inventory
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryVisible = !inventoryVisible;
            if (inventoryVisible)
            {
                currentSelection = 0;
            }
        }
        
        InventoryAnim.SetBool("inventoryVisible",inventoryVisible);

        //adjust the current selection
        if (lastOperationisDown)
        {
            updateSelection();
        }
        else
        {
            updateSelectionUpward();
        }
        
        //allows moving through the inventory
        processInput();
    }

    /// <summary>
    /// makes sure all slots stay in the range they should be which should be between 0 and the player inventory count
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
        if(p_inventory.getCount()==1)
        {
            displayItems[index] = 0;
        }
        else
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
    }

    /// <summary>
    /// returns true if the slot index is in range, false if now
    /// </summary>
    /// <param name="index">the slot to check</param>
    /// <returns>is the slot in range or nah?</returns>
    private bool slotInRange(int index)
    {
        if (displayItems[index] >= 0 && displayItems[index] < p_inventory.getCount())
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
    public void moveRight()
    {
        if (!playerInventoryEmpty)
        {
            for (int i = 0; i < displayItems.Count; i++)
            {
                displayItems[i]--;
            }
            processSlots();
            if (scrollLeft != null)
            {
                scrollLeft.Invoke();
            }
        }
    }

    /// <summary>
    /// moves all slots one to the right
    /// </summary>
    [Button]
    public void moveLeft()
    {
        if (!playerInventoryEmpty)
        {
            for (int i = 0; i < displayItems.Count; i++)
            {
                displayItems[i]++;
            }
            processSlots();
            if (scrollRight != null)
            {
                scrollRight.Invoke();
            }
        }
    }

    /// <summary>
    /// gets a value out of slots
    /// </summary>
    /// <param name="slot">the index to pull from</param>
    /// <returns>the item index</returns>
    public int getIndex(int slot)
    {
        return displayItems[slot];
    }

    /// <summary>
    /// reset the bulk of the data
    /// </summary>
    public void RefreshAll()
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

        if (refresh != null)
        {
            if (refresh != null)
            {
                refresh.Invoke();
            }
        }
    }

    /// <summary>
    /// update the current selection moving downward
    /// </summary>
    private void updateSelection()
    {
        //if the item is valid
        if (CurrentItem != null)
        {
            if (currentSelection != 0)
            {
                switch (currentSelection)
                {
                    case InventoryUISelection.UseEquip:
                        UpdateButtons(true,false,false);
                        if (!CurrentItem.equippable && !CurrentItem.usable)
                        {
                            currentSelection++;
                        }
                        break;
                    case InventoryUISelection.Discard:
                        UpdateButtons(false,false,true);
                        if (!CurrentItem.discardable)
                        {
                            currentSelection++;
                        }
                        break;
                    case InventoryUISelection.Combine:
                        UpdateButtons(false,true,false);
                        if (!CurrentItem.combineable)
                        {
                            currentSelection++;
                        }
                        break;
                    case InventoryUISelection.Def:
                        currentSelection = InventoryUISelection.ScrollList;
                        UpdateButtons(false,false,false);
                        break;
                    default:
                        currentSelection = InventoryUISelection.ScrollList;
                        UpdateButtons(false,false,false);
                        break;
                }
            }
        }
    }
    
    /// <summary>
    /// update the current selection, moving upward if the value is invalid
    /// </summary>
    private void updateSelectionUpward()
    {
        //if the item is valid
        if (CurrentItem != null)
        {
            if (currentSelection != 0)
            {
                switch (currentSelection)
                {
                    case InventoryUISelection.UseEquip:
                        UpdateButtons(true,false,false);
                        if (!CurrentItem.equippable && !CurrentItem.usable)
                        {
                            currentSelection--;
                        }
                        break;
                    case InventoryUISelection.Discard:
                        UpdateButtons(false,false,true);
                        if (!CurrentItem.discardable)
                        {
                            currentSelection--;
                        }
                        break;
                    case InventoryUISelection.Combine:
                        UpdateButtons(false,true,false);
                        if (!CurrentItem.combineable)
                        {
                            currentSelection--;
                        }
                        break;
                    case InventoryUISelection.Def:
                        currentSelection = InventoryUISelection.ScrollList;
                        UpdateButtons(false,false,false);
                        break;
                    default:
                        currentSelection = InventoryUISelection.ScrollList;
                        UpdateButtons(false,false,false);
                        break;
                }
            }
        }

        
    }
    
    /// <summary>
    /// update the buttons visiblilty for current selection
    /// </summary>
    /// <param name="equipUse"></param>
    /// <param name="combine"></param>
    /// <param name="discard"></param>
    private void UpdateButtons(bool equipUse, bool combine, bool discard )
    {
        Color darkColor = new Color(0.2f, 0.2f, 0.2f, 1);
        Color lightColor = new Color(0.8f, 0.8f, 0.8f, 1);
        if (equipUse)
        {
            useEquipButton.Color = lightColor;
            useEquipText.Color = darkColor;
        }
        else
        {
            useEquipButton.Color = darkColor;
            useEquipText.Color = lightColor;
        }
        
        if (combine)
        {
            combineButton.Color = lightColor;
            combineText.Color = darkColor;
        }
        else
        {
            combineButton.Color = darkColor;
            combineText.Color = lightColor;
        }
        
        if (discard)
        {
            discardButton.Color = lightColor;
            DiscardText.Color = darkColor;
        }
        else
        {
            discardButton.Color = darkColor;
            DiscardText.Color = lightColor;
        }
    }

    /// <summary>
    /// process input for moving through the inventory UI
    /// </summary>
    private void processInput()
    {
        if (inventoryVisible)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                currentSelection++;
                updateSelection();
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                currentSelection--;
                updateSelectionUpward();
            }

            if (currentSelection == InventoryUISelection.ScrollList)
            {
                if (Input.GetKeyUp(KeyCode.D))
                {
                    moveRight();
                }

                if (Input.GetKeyUp(KeyCode.A))
                {
                    moveLeft();
                }
            }
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