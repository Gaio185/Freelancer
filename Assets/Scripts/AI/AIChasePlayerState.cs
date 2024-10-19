using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChasePlayerState : AiState
{
    public Transform playerTransform;

    public AiStateId GetId()
    {
        return AiStateId.Chase;
    }

    public void Enter(AiAgent agent)
    {
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

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
    }

    public void Exit(AiAgent agent)
    {

    }

    

    
}
