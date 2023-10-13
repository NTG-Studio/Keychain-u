using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class inventoryScrollOptionsState : FiniteState<InventoryStates>
{
    public override void SetupState(InventoryStates stateKey, FiniteStateMachine<InventoryStates> master)
    {
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
    }

    public override void ExitState()
    {
    }

    public override void process_input(InputType type)
    {
        switch (type)
        {
            case InputType.Right:
                NextState(InventoryStates.ScrollItems);
                break;
            case InputType.Up:
                break;
            case InputType.Down:
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