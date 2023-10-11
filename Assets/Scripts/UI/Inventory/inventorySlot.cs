using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nova;
using UnityEngine;

public class inventorySlot : MonoBehaviour
{
    /// <summary>
    /// the slot in the lineup that this slot is targeting
    /// </summary>
    [Range(0, 6)] [SerializeField] private int slotTarget;

    /// <summary>
    /// a list of all the slot targets
    /// </summary>
    [SerializeField] private List<Transform> slots;

    /// <summary>
    /// the prefix for the name of the slots
    /// </summary>
    [SerializeField] private string slotPrefix = "slot";

    /// <summary>
    /// the number of slots in the inventory
    /// </summary>
    [SerializeField] private int numberOfSlots = 7;

    /// <summary>
    /// the position for this element to target
    /// </summary>
    [SerializeField] private Vector3 targetPosition;

    /// <summary>
    /// the scale that we want to target
    /// </summary>
    [SerializeField] private Vector3 targetScale;


    /// <summary>
    /// a reference to the players inventory
    /// </summary>
    [SerializeField] private playerInventory p_inventory;

    /// <summary>
    /// the ui manager to determine what item we are currently grabbing
    /// </summary>
    [SerializeField] private inventoryUIManager ui_manager;

    /// <summary>
    /// the current item held by this slot
    /// </summary>
    [Expandable] [SerializeField] private item current_item;

    /// <summary>
    /// a link to the image to apply the sprite texture to 
    /// </summary>
    [SerializeField] private UIBlock2D novaUIImage;

    /// <summary>
    /// data for the items name display
    /// </summary>
    [SerializeField] private TextBlock itemName;
    
    /// <summary>
    /// data for the items description
    /// </summary>
    [SerializeField] private TextBlock itemDescription;

    [BoxGroup("Buttons")] [SerializeField] private UIBlock2D useEquipButton;
    [BoxGroup("Buttons")] [SerializeField] private TextBlock useEquipText;
    [BoxGroup("Buttons")] [SerializeField] private UIBlock2D combineButton;
    [BoxGroup("Buttons")] [SerializeField] private TextBlock combineText;
    [BoxGroup("Buttons")] [SerializeField] private UIBlock2D discardButton;
    [BoxGroup("Buttons")] [SerializeField] private TextBlock DiscardText;
    // Start is called before the first frame update
    void Start()
    {
        //find all the slots
        slots = new List<Transform>();
        for (int i = -1; i < numberOfSlots; i++)
        {
            slots.Add(GameObject.Find(slotPrefix + i).transform);
        }

        //snap it to its position at start
        transform.position = slots[slotTarget].position;
        
        //get references to managers
        if (p_inventory == null)
        {
            p_inventory = GameObject.FindObjectOfType<playerInventory>();
        }

        if (ui_manager == null)
        {
            ui_manager = GameObject.FindObjectOfType<inventoryUIManager>();
        }
        updateItem();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = slots[slotTarget].position;
        transform.position = smoothLerp(transform.position, targetPosition, 25);

        if (slotTarget == 1 || slotTarget == 5)
        {
            targetScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            if (slotTarget == 0 || slotTarget == 6)
            {
                targetScale = new Vector3(0, 0, 0);
            }
            else
            {
                targetScale = new Vector3(1f, 1f, 1f);
            }
        }

        transform.localScale = smoothLerp(transform.localScale, targetScale, 15f);
        
        updateImage();
    }

    /// <summary>
    /// smoothly interpolates between two values (exponential)
    /// </summary>
    /// <param name="from">a vector3 of your current value</param>
    /// <param name="to">a vector3 of the value you are targeting</param>
    /// <param name="sharpness">the speed of the lerp</param>
    /// <returns>the new value</returns>
    private Vector3 smoothLerp(Vector3 from, Vector3 to, float sharpness)
    {
        return Vector3.Lerp(from, to, 1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }

    /// <summary>
    /// moves the object one slot to the left
    /// </summary>
    [Button]
    public void moveRight()
    {
        if (slotTarget == 0)
        {
            slotTarget=slots.Count-1;
        }
        else
        {
            slotTarget--;
        }
        updateItem();
    }


    /// <summary>
    /// moves the object one slot to the right
    /// </summary>
    [Button]
    public void moveLeft()
    {
        if (slotTarget > numberOfSlots-1)
        {
            slotTarget = 0;
        }
        else
        {
            slotTarget++;
        }
        updateItem();
    }
    
    /// <summary>
    /// Updates the item controlled by this slot
    /// </summary>
    public void updateItem()
    {
        if (p_inventory.getCount() > 0)
        {
            try
            {
                current_item = p_inventory.getItemByIndex(ui_manager.getIndex(slotTarget)).itm;
                updateImage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        else
        {
            ui_manager.CurrentItem = null;
            updateImage();
        }

        if (current_item == null)
        {
            novaUIImage.Color = new Color(0, 0, 0, 0);
        }
        else
        {
            novaUIImage.Color = new Color(1, 1, 1, 1);
        }
    }

    /// <summary>
    /// apply the image to the ui block
    /// </summary>
    private void updateImage()
    {
        if (novaUIImage == null)
        {
            novaUIImage = GetComponent<UIBlock2D>();
        }

        //if setting succeeded
        if (novaUIImage != null)
        {
            if (current_item != null)
            {
                novaUIImage.SetImage(current_item.image);
                novaUIImage.Color = new Color(1, 1, 1, 1);
            }
            else
            {
                novaUIImage.Color = new Color(0, 0, 0, 0);
            }
        }

        updateData();
    }

    /// <summary>
    /// reinitialize the slot
    /// </summary>
    public void refresh()
    {
        Debug.Log("refresh");
        updateItem();
    }

    /// <summary>
    /// update the name,description,and buttons
    /// </summary>
    private void updateData()
    {
        if (slotTarget == 3)
        {
            if(current_item!=null)
            {
                //update uiManager
                ui_manager.CurrentItem = current_item;

                //name and description
                itemName.Text = current_item.item_name;
                itemDescription.Text = current_item.description;

                //buttons

                //usable
                if (current_item.usable)
                {
                    useEquipButton.gameObject.SetActive(true);
                    useEquipText.gameObject.SetActive(true);
                    useEquipText.Text = "Use";
                }
                else
                {
                    useEquipButton.gameObject.SetActive(false);
                    useEquipText.gameObject.SetActive(false);
                }

                //equippable
                if (current_item.equippable)
                {
                    useEquipButton.gameObject.SetActive(true);
                    useEquipText.gameObject.SetActive(true);
                    useEquipText.Text = "Equip";
                }
                else
                {
                    if (!current_item.usable)
                    {
                        useEquipButton.gameObject.SetActive(false);
                        useEquipText.gameObject.SetActive(false);
                    }
                }

                //discardable
                if (current_item.discardable)
                {
                    discardButton.gameObject.SetActive(true);
                    DiscardText.gameObject.SetActive(true);
                }
                else
                {
                    discardButton.gameObject.SetActive(false);
                    DiscardText.gameObject.SetActive(false);
                }

                //combinable
                if (current_item.combineable)
                {
                    combineButton.gameObject.SetActive(true);
                    combineText.gameObject.SetActive(true);
                }
                else
                {
                    combineButton.gameObject.SetActive(false);
                    combineText.gameObject.SetActive(false);
                }
            }
            else
            {
                useEquipButton.gameObject.SetActive(false);
                useEquipText.gameObject.SetActive(false);
            
                discardButton.gameObject.SetActive(false);
                DiscardText.gameObject.SetActive(false);
            
                combineButton.gameObject.SetActive(false);
                combineText.gameObject.SetActive(false);
            }
        }
        else
        {
            useEquipButton.gameObject.SetActive(false);
            useEquipText.gameObject.SetActive(false);
            
            discardButton.gameObject.SetActive(false);
            DiscardText.gameObject.SetActive(false);
            
            combineButton.gameObject.SetActive(false);
            combineText.gameObject.SetActive(false);
        }
    }
}


