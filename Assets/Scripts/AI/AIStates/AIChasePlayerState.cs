using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIHuntPlayerState : AiState
{
    public Transform playerTransform;

    public AiStateId GetId()
    {
        return AiStateId.Hunt;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Hunt");

        if(playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        agent.detection.isMoving = true;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.navMeshAgent.enabled) { return; }

        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = playerTransform.position;
        }

        Vector3 direction = playerTransform.position - agent.navMeshAgent.destination;
        direction.y = 0;

        if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
        {
            if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
            {
                agent.navMeshAgent.destination = playerTransform.position;
            }
        }

        if (Vector3.Distance(agent.transform.position, playerTransform.position) > agent.detection.awarenessRadius)
        {
            Debug.Log("LeftHuntState");
            agent.navMeshAgent.ResetPath();
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
        
    }

    public void Exit(AiAgent agent)
    {
    }

    

    
}
