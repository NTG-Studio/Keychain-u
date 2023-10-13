using System;
using UnityEngine;

namespace State
{
    public abstract class FiniteState<eState> where eState : Enum
    {
        public eState StateKey;
        protected FiniteStateMachine<eState> _master;

        public virtual void SetupState(eState stateKey, FiniteStateMachine<eState> master)
        {
            StateKey = stateKey;
            _master = master;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public virtual void NextState(eState nextStateKey)
        {
            _master.currentState = nextStateKey;
            ExitState();
            _master.States[nextStateKey].EnterState();
        }

        public abstract void process_input(InputType type);
    
        public abstract void CollisionEnter(GameObject o);
        public abstract void CollisionExit(GameObject o);
        public abstract void TriggerEnter(GameObject o);
        public abstract void TriggerExit(GameObject o);
    }

    public enum InputType
    {
        Up,
        Down,
        Left,
        Right,
        LeftShoulder,
        RightShoulder,
        Confirm,
        Cancel,
        Pause,
        Reload,
        Use
    }
}