using State;
using UnityEngine;

public class MapState : FiniteState<PauseStates>
{
    public override void EnterState()
    {
        Debug.Log("Map");
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void process_input(InputType type)
    {
        if (type == InputType.Pause)
        {
            NextState(PauseStates.Hidden);
        }

        if (type == InputType.LeftShoulder)
        {
            NextState(PauseStates.Journal);
        }
        if (type == InputType.RightShoulder)
        {
            NextState(PauseStates.Inventory);
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

    public override void SetupState(PauseStates stateKey, FiniteStateMachine<PauseStates> master)
    {
        base.SetupState(stateKey, master);
    }

    public override void NextState(PauseStates nextStateKey)
    {
        base.NextState(nextStateKey);
    }
}
