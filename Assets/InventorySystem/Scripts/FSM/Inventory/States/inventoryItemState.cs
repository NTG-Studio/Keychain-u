using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class inventoryItemState : FiniteState<InventoryStates>
{
    public ItemTextController[] Texts;
    public string textForPrefix = "itemNameDisplay";
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
            case InputType.Left:
                NextState(InventoryStates.ScrollOptions);
                break;
            case InputType.Up:
                foreach (var t in Texts)
                {
                    t.MoveBack();
                }
                break;
            case InputType.Down:
                foreach (var t in Texts)
                {
                    t.MoveForward();
                }
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