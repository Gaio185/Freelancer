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
        agent.detection.lightRef.color = Color.green;
        agent.detection.lightRef.enabled = true;
        agent.visorMaterial.color = Color.green;
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

        if (agent.detection.canSeePlayer && !agent.detection.playerDetected && agent.detection.shouldDetect)
        {
            agent.detection.lightRef.color = Color.yellow;
            agent.visorMaterial.color = Color.yellow;
        }
        else
        {
            agent.detection.lightRef.color = Color.green;
            agent.visorMaterial.color = Color.green;
        }
    }

    public void Exit(TOAgent agent)
    {
   
    }
 
}
