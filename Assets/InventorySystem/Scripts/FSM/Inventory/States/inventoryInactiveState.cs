using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class inventoryInactiveState : FiniteState<InventoryStates>
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
    {;
    }

    public override void ExitState()
    {
    }

    public override void process_input(InputType type)
    {
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
