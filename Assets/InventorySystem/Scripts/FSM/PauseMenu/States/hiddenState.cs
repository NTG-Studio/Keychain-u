using State;
using UnityEngine;

public class HiddenState : FiniteState<PauseStates>
{
    public override void EnterState()
    {
        PauseFSM f = (PauseFSM)_master;
        f.toggle_Anim(false);
        Debug.Log("hidden");
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void process_input(InputType type)
    {
        PauseFSM i = _master as PauseFSM;
        if (type == InputType.Pause)
        {
            i.openSound.Play();
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
