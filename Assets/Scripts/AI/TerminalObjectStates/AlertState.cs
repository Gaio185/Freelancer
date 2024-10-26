using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : TOState
{
    public float timer;

    public TOStateId GetId()
    {
        return TOStateId.Alert;
    }

    public void Enter(TOAgent agent)
    {
        Debug.Log("Alert");
        for (int i = 0; i < agent.agents.Length; i++)
        {
            agent.agents[i].stateMachine.ChangeState(AiStateId.Investigate);
        }
        timer = agent.config.searchTime;
    }

    public void Update(TOAgent agent)
    {
        if(!agent.detection.canSeePlayer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                agent.detection.playerDetected = false;
                agent.stateMachine.ChangeState(TOStateId.On);
            }
        }
        else
        {
            timer = agent.config.searchTime;
        }
        
    }

    public void Exit(TOAgent agent)
    {

    }
}
