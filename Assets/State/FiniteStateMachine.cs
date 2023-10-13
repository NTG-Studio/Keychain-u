using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public abstract class FiniteStateMachine<eState> : MonoBehaviour  where eState : Enum
{
    public Dictionary<eState, FiniteState<eState>> States;
    public eState currentState;
    
    /// <summary>
    /// set up the state machine and load all data into it
    /// </summary>
    public virtual void Start()
    {
        initializeFSM();
        foreach (var s in States)
        {
            s.Value.SetupState(s.Key,this);
        }
        States[currentState].EnterState();
    }

    /// <summary>
    /// Initialize the state machine assigning all of the nessesary states
    /// </summary>
    public abstract void initializeFSM();

    

    // Update is called once per frame
    public virtual void Update()
    {
        States[currentState].UpdateState();
    }
}
