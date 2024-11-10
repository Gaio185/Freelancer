using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStateMachine : MonoBehaviour
{
    public WorkerState[] states;
    public WorkerAgent agent;
    public WorkerStateId currentState;

    public WorkerStateMachine(WorkerAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        states = new WorkerState[numStates];
    }

    public void RegisterStates(WorkerState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public WorkerState GetState(WorkerStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(WorkerStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
