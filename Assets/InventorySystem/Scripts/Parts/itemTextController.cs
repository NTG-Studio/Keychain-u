using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Nova;
using UnityEngine;

public class ItemTextController : MonoBehaviour
{
    [SerializeField] private string displayString = "";
    [SerializeField] private int targetIndex = 0;
    [SerializeField][Required] private Transform[] transformTargets;
    [SerializeField] [Required] private TextBlock uiBlock;
    
    private static readonly Color visible = new Color(1, 1, 1, 1);
    private static readonly Color hidden = new Color(1, 1, 1, 0);

    public playerInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = transformTargets[targetIndex].position;
        inventory = GameObject.FindObjectOfType<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNode();
    }

    private void UpdateNode()
    {
        ClampIndex();
        UpdatePosition();
        AdjustOpacity();
        updateText();
    }
    
    /// <summary>
    /// make sure to keep the target index in range
    /// </summary>
    void ClampIndex()
    {
        if (targetIndex < 0)
        {
            targetIndex = 8;
        }

        if (targetIndex >= 9)
        {
            targetIndex = 0;
        }
    }

    void UpdatePosition()
    {
        transform.position = MyMath.SmoothLerp(transform.position, transformTargets[targetIndex].position, 25f);
    }

    void AdjustOpacity()
    {
        if (targetIndex == 8 || targetIndex == 0)
        {
            uiBlock.Color = MyMath.SmoothLerp(uiBlock.Color, hidden, 25f);
        }
        else
        {
            uiBlock.Color = MyMath.SmoothLerp(uiBlock.Color, visible, 25f);
        }

        if (targetIndex == 5)
        {
            transform.localScale = MyMath.SmoothLerp(transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 25f);
        }
        else
        {
            transform.localScale = MyMath.SmoothLerp(transform.localScale, new Vector3(.8f, .8f, .8f), 25f);
        }
}

    [Button]
    public void MoveForward()
    {
        targetIndex++;
    }
    
    [Button]
    public void MoveBack()
    {
        targetIndex--;
    }

    public void updateText()
    {
        uiBlock.Text = inventory.inventory[inventory.slots[targetIndex]].itm.itemName;
    }
}
