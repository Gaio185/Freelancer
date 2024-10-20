using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalObjectStateMachine 
{
    public TOState[] states;
    public TOAgent agent;
    public TOStateId currentState;

    public TerminalObjectStateMachine(TOAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(TOStateId)).Length;
        states = new TOState[numStates];
    }

    public void RegisterStates(TOState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public TOState GetState(TOStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(TOStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
