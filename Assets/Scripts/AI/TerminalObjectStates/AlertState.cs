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
            if (agent.agents[i].stateMachine.currentState != AiStateId.Hunt &&
                agent.agents[i].stateMachine.currentState != AiStateId.Shoot &&
                agent.agents[i].stateMachine.currentState != AiStateId.Stunned) agent.agents[i].stateMachine.ChangeState(AiStateId.Investigate);
        }
        timer = agent.config.searchTime;
        agent.timeElapsed = 0;
        agent.material.SetColor("_EmissionColor", Color.red);
        agent.detection.lightRef.color = Color.red;
    }

    public void Update(TOAgent agent)
    {
        if(!agent.detection.canSeePlayer)
        {
            agent.material.SetColor("_EmissionColor", Color.yellow);
            agent.detection.lightRef.color = Color.yellow;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                agent.detection.playerDetected = false;
                agent.stateMachine.ChangeState(TOStateId.On);
            }
        }
        else
        {
            agent.material.SetColor("_EmissionColor", Color.red);
            agent.detection.lightRef.color = Color.red;
            timer = agent.config.searchTime;
        }
        
    }

    public void Exit(TOAgent agent)
    {

    }
}
