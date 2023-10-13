using State;
using UnityEngine;

public class JournalState : FiniteState<PauseStates>
{
    public override void EnterState()
    {
        Debug.Log("Journal");
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
            NextState(PauseStates.Inventory);
        }
        if (type == InputType.RightShoulder)
        {
            NextState(PauseStates.Map);
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
