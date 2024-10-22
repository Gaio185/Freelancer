using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnState : TOState
{
    public TOStateId GetId()
    {
        return TOStateId.On;
    }

    public void Enter(TOAgent agent)
    {
        Debug.Log("On");
    }

    public void Update(TOAgent agent)
    {
        if(agent.detection.playerDetected && agent.tag == "Camera")
        {
            agent.stateMachine.ChangeState(TOStateId.Alert);
        }
        else if(agent.detection.playerDetected)
        {
            agent.stateMachine.ChangeState(TOStateId.Shoot);
        }
    }

    public void Exit(TOAgent agent)
    {
   
    }
 
}
