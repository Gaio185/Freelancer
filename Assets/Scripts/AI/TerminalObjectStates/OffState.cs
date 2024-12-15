using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffState : TOState
{
    public TOStateId GetId()
    {
        return TOStateId.Off;
    }

    public void Enter(TOAgent agent)
    {
        Debug.Log("Off");
        agent.detection.lightRef.enabled = false;
    }

    public void Update(TOAgent agent)
    {
        agent.detection.canSeePlayer = false;
    }

    public void Exit(TOAgent agent)
    {

    }
}

    

