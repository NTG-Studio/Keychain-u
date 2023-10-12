using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Nova;
using UnityEngine;
using UnityEngine.Serialization;
using Items;

namespace UI.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        
        #region Params

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
        [FormerlySerializedAs("p_inventory")] [SerializeField]
        private PlayerInventory pInventory;

        /// <summary>
        /// the ui manager to determine what item we are currently grabbing
        /// </summary>
        [FormerlySerializedAs("ui_manager")] [SerializeField]
        private InventoryUIManager uiManager;

        /// <summary>
        /// the current item held by this slot
        /// </summary>
        [FormerlySerializedAs("current_item")] [Expandable] [SerializeField]
        private Item currentItem;

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
        
        [Header("References")]
     
        public UINode useEquip;
        public UINode combine;
        public UINode discard;

        [FormerlySerializedAs("DiscardText")] [BoxGroup("Buttons")] [SerializeField]
        private TextBlock discardText;

        #endregion

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
            if (pInventory == null)
            {
                pInventory = GameObject.FindObjectOfType<PlayerInventory>();
            }

            if (uiManager == null)
            {
                uiManager = GameObject.FindObjectOfType<InventoryUIManager>();
            }

            UpdateItem();

            uiManager.slots.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
            targetPosition = slots[slotTarget].position;
            transform.position = SmoothLerp(transform.position, targetPosition, 25);

            if (slotTarget is 1 or 5)
            {
                targetScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                targetScale = slotTarget is 0 or 6 ? new Vector3(0, 0, 0) : new Vector3(1f, 1f, 1f);
            }

            transform.localScale = SmoothLerp(transform.localScale, targetScale, 15f);

            UpdateImage();
        }

        /// <summary>
        /// smoothly interpolates between two values (exponential)
        /// </summary>
        /// <param name="from">a vector3 of your current value</param>
        /// <param name="to">a vector3 of the value you are targeting</param>
        /// <param name="sharpness">the speed of the lerp</param>
        /// <returns>the new value</returns>
        private Vector3 SmoothLerp(Vector3 from, Vector3 to, float sharpness)
        {
            return Vector3.Lerp(from, to, 1f - Mathf.Exp(-sharpness * Time.deltaTime));
        }

        /// <summary>
        /// moves the object one slot to the left
        /// </summary>
        [Button]
        public void MoveRight()
        {
            if (slotTarget == 0)
            {
                slotTarget = slots.Count - 1;
            }
            else
            {
                slotTarget--;
            }

            UpdateItem();
        }


        /// <summary>
        /// moves the object one slot to the right
        /// </summary>
        [Button]
        public void MoveLeft()
        {
            if (slotTarget > numberOfSlots - 1)
            {
                slotTarget = 0;
            }
            else
            {
                slotTarget++;
            }

            UpdateItem();
        }

        /// <summary>
        /// Updates the item controlled by this slot
        /// </summary>
        void UpdateItem()
        {
            if (pInventory != null && pInventory.GetCount() > 0)
            {
                try
                {
                    currentItem = pInventory.GetItemByIndex(uiManager.GetIndex(slotTarget)).Itm;
                    UpdateImage();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            else
            {
                if (uiManager != null)
                {
                    uiManager.currentItem = null;
                }

                UpdateImage();
            }

            novaUIImage.Color = currentItem is null ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 1);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// apply the image to the ui block
        /// </summary>
        private void UpdateImage()
        {
            novaUIImage ??= GetComponent<UIBlock2D>();

            //if setting succeeded
            if (novaUIImage is not null)
            {
                if (currentItem is not null)
                {
                    novaUIImage.SetImage(currentItem.image);
                    novaUIImage.Color = new Color(1, 1, 1, 1);
                }
                else
                {
                    novaUIImage.Color = new Color(0, 0, 0, 0);
                }
            }

            UpdateData();
        }

        /// <summary>
        /// reinitialize the slot
        /// </summary>
        public void Refresh()
        {
            UpdateItem();
        }

        /// <summary>
        /// update the name,description,and buttons
        /// </summary>
        private void UpdateData()
        {
            if (slotTarget == 3)
            {
                if (currentItem is not null)
                {
                    //update uiManager
                    uiManager.currentItem = currentItem;

                    //name and description
                    itemName.Text = currentItem.itemName;
                    itemDescription.Text = currentItem.description;

                    //buttons

                    //usable
                    if (currentItem.usable)
                    {
                        useEquip.EnableAndSetText("Use");
                    }
                    else
                    {
                        useEquip.Disable();
                    }

                    //equippable
                    if (currentItem.equippable)
                    {
                        useEquip.EnableAndSetText("Equip");
                    }
                    else
                    {
                        if (!currentItem.usable)
                        {
                            useEquip.Disable();
                        }
                    }

                    //discardable
                    if (currentItem.discardable)
                    {
                        discard.Enable();
                    }
                    else
                    {
                        discard.Disable();
                    }

                    //combinable
                    if (currentItem.combineable)
                    {
                        combine.Enable();
                    }
                    else
                    {
                        combine.Disable();
                    }
                }
                else
                {
                   useEquip.Disable();
                   discard.Disable();
                   combine.Disable();
                }
            }
            else
            {
                useEquip.Disable();
                discard.Disable();
                combine.Disable();
            }
        }
    }
}