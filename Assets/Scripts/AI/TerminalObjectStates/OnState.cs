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
        if(agent.detection.playerDetected)
        {
            agent.stateMachine.ChangeState(TOStateId.Alert);
        }
    }

    public void Exit(TOAgent agent)
    {
   
    }
 
}
