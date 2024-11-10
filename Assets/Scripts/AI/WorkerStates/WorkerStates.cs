using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkerStateId
{
    Wander,
    Alert
}

public interface WorkerState
{
    WorkerStateId GetId();
    void Enter(WorkerAgent agent);
    void Update(WorkerAgent agent);
    void Exit(WorkerAgent agent);
}
