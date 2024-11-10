using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAlertState : WorkerState
{
    WorkerStateId WorkerState.GetId()
    {
        return WorkerStateId.Alert;
    }

    public void Enter(WorkerAgent agent)
    {
    }

    public void Update(WorkerAgent agent)
    {
    }

    public void Exit(WorkerAgent agent)
    {
       
    } 
}
