using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class inventoryItemState : FiniteState<InventoryStates>
{
    public ItemTextController[] Texts;
    public string textForPrefix = "itemNameDisplay";
    public playerInventory inventory;

    [Header("Buttons")] 
    public GameObject UseButton;
    public GameObject CombineButton;
    public GameObject DiscardButton;

    [Header("Data")] 
    public PlayerInventorySlot currentSlot;
    public override void SetupState(InventoryStates stateKey, FiniteStateMachine<InventoryStates> master)
    {
        Debug.Log("inventor item setup");
        Texts = new ItemTextController[]
        {
            GameObject.Find(textForPrefix + "0").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "1").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "2").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "3").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "4").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "5").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "6").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "7").GetComponent<ItemTextController>(),
            GameObject.Find(textForPrefix + "8").GetComponent<ItemTextController>()
        };

        inventory = GameObject.FindObjectOfType<playerInventory>();
        
        UseButton = GameObject.Find("UseButton");
        CombineButton = GameObject.Find("CombineButton");
        DiscardButton = GameObject.Find("DiscardButton");
        
        base.SetupState(stateKey, master);
    }

    public override void NextState(InventoryStates nextStateKey)
    {
        base.NextState(nextStateKey);
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        if (inventory.slots[0] != -1)
        {
            currentSlot = inventory.inventory[inventory.slots[5]];

            if (currentSlot.itm.canUse || currentSlot.itm.canEquip)
            {
                UseButton.SetActive(true);
            }
            else
            {
                UseButton.SetActive(false);
            }

            if (currentSlot.itm.canCombine)
            {
                CombineButton.SetActive(true);
            }
            else
            {
                CombineButton.SetActive(false);
            }

            if (currentSlot.itm.canDiscard)
            {
                DiscardButton.SetActive(true);
            }
            else
            {
                DiscardButton.SetActive(false);
            }
        }
        else
        {
            UseButton.SetActive(false);
            CombineButton.SetActive(false);
            DiscardButton.SetActive(false);
        }
    }

    public override void ExitState()
    {
        
    }

    public override void process_input(InputType type)
    {
        inventoryFSM i = _master as inventoryFSM;
        switch (type)
        {
            case InputType.Left:
                NextState(InventoryStates.ScrollOptions);
                i.leftRightSound.Play();
                break;
            case InputType.Up:
                foreach (var t in Texts)
                {
                    t.MoveBack();
                }
                i.upDownSound.Play();
                inventory.ShiftInventory(true);
                break;
            case InputType.Down:
                foreach (var t in Texts)
                {
                    t.MoveForward();
                }
                
                i.upDownSound.Play();
                inventory.ShiftInventory(false);
                break;
        }
    }

    public override void CollisionEnter(GameObject o)
    {
    }

    public override void CollisionExit(GameObject o)
    {
    }

    public override void TriggerEnter(GameObject o)
    {
    }

    public override void TriggerExit(GameObject o)
    {
    }
}