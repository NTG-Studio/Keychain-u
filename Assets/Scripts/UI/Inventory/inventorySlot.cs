using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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
    /// 
    /// </summary>
    [SerializeField] private item current_item;
    // Start is called before the first frame update
    void Start()
    {
        //find all the slots
        slots = new List<Transform>();
        for (int i = -1; i < numberOfSlots; i++)
        {
            slots.Add(GameObject.Find(slotPrefix + i).transform);
        }

        if (p_inventory == null)
        {
            p_inventory = GameObject.FindObjectOfType<playerInventory>();
        }
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

        transform.localScale = smoothLerp(transform.localScale, targetScale, 25f);
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
    public void moveLeft()
    {
        if (slotTarget == 0)
        {
            slotTarget=slots.Count-1;
        }
        else
        {
            slotTarget--;
        }
    }


    /// <summary>
    /// moves the object one slot to the right
    /// </summary>
    [Button]
    public void moveRight()
    {
        if (slotTarget >= numberOfSlots)
        {
            slotTarget = 0;
        }
        else
        {
            slotTarget++;
        }
    }
    
    /// <summary>
    /// Updates the item controlled by this slot
    /// </summary>
    public void updateItem()
    {
        
    }
}


