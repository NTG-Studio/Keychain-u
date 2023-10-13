using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class inventoryFSM : FiniteStateMachine<InventoryStates>
{
    private PauseFSM parentFSM;
    public override void initializeFSM()
    {
        States = new Dictionary<InventoryStates, FiniteState<InventoryStates>>();
        
        States.Add(InventoryStates.Inactive,new inventoryInactiveState());
        States.Add(InventoryStates.ScrollItems,new inventoryItemState());
        States.Add(InventoryStates.ScrollOptions,new inventoryScrollOptionsState());

        currentState = InventoryStates.Inactive;
    }

    public override void Start()
    {
        parentFSM = GameObject.FindObjectOfType<PauseFSM>();
        base.Start();
    }

    public override void Update()
    {
        if (parentFSM.currentState == PauseStates.Inventory)
        {
            base.Update();
        }
        else
        {
            if (currentState != InventoryStates.Inactive)
            {
                currentState = InventoryStates.Inactive;
            }
        }
    }

    public void enterInventory()
    {
        States[currentState].NextState(InventoryStates.ScrollItems);
    }

    public void OnRight()
    {
        States[currentState].process_input(InputType.Right);
    }

    public void OnLeft()
    {
        States[currentState].process_input(InputType.Left);
    }

    public void OnUp()
    {
        States[currentState].process_input(InputType.Up);
    }

    public void OnDown()
    {
        States[currentState].process_input(InputType.Down);
    }

    public void OnUse()
    {
        States[currentState].process_input(InputType.Use);
    }
}
