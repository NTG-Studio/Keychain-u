using System.Collections.Generic;
using State;
using UnityEngine;

public class PauseFSM : FiniteStateMachine<PauseStates>
{
    [SerializeField] private Animator uiAnim;

    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource tabSound;
    
    public override void initializeFSM()
    {
        //set up all of our states
        States = new Dictionary<PauseStates, FiniteState<PauseStates>>();
        
        States.Add(PauseStates.Hidden, new HiddenState());
        States.Add(PauseStates.Inventory, new inventoryState());
        States.Add(PauseStates.Map, new MapState());
        States.Add(PauseStates.Journal, new JournalState());

        currentState = PauseStates.Hidden;
    }

    public void toggle_Anim(bool anim)
    {
        uiAnim.SetBool("show",anim);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void LeftShoulder()
    {
        States[currentState].process_input(InputType.LeftShoulder);
    }

    public void RightShoulder()
    {
        States[currentState].process_input(InputType.RightShoulder);
    }

    public void InventoryButton()
    {
        States[currentState].process_input(InputType.Pause);
    }
}
