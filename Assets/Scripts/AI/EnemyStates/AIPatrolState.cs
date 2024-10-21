using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolState : AiState
{
    public int targetPoint;

    public AiStateId GetId()
    {
       return AiStateId.Patrol;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Patrol");
        agent.navMeshAgent.isStopped = false;
        targetPoint = 0;
    }

    public void Update(AiAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance <= 0.1f)
        {
            targetPoint++;
            if (targetPoint >= agent.patrolPoints.Length)
            {
                targetPoint = 0;
            }
        }
        agent.navMeshAgent.destination = agent.patrolPoints[targetPoint].position;

        if (agent.detection.canSeePlayer)
        {
            agent.navMeshAgent.isStopped = true;
        }
        else
        {
           agent.navMeshAgent.isStopped = false;
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
