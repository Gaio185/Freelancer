using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AIIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Idle");
        agent.navMeshAgent.destination = agent.startingPosition;
        agent.navMeshAgent.isStopped = false;
    }

    public void Update(AiAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance < 0.1f)
        {
            agent.detection.isMoving = false;
        }
        if (agent.detection.playerDetected)
        {
            agent.stateMachine.ChangeState(AiStateId.Hunt);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
