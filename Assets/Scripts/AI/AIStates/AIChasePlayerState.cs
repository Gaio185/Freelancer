using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIHuntPlayerState : AiState
{
    public Transform playerTransform;
    float timer = 0.0f;
    float huntTimer = 0.0f;

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

        huntTimer = agent.config.searchTime;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.navMeshAgent.enabled) { return; }

        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = playerTransform.position;
        }

        if(timer < 0.0f)
        {
            Vector3 direction = playerTransform.position - agent.navMeshAgent.destination;
            direction.y = 0;

            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = playerTransform.position;
                }
            }
            timer = agent.config.maxTime;
        }   

        if (Vector3.Distance(agent.transform.position, playerTransform.position) > agent.detection.awarenessRadius)
        {
            huntTimer -= Time.deltaTime;
            if(huntTimer < 0.0f)
            {
                Debug.Log("LeftHuntState");
                agent.navMeshAgent.ResetPath();
                agent.stateMachine.ChangeState(AiStateId.Idle);
            }
        }
        else
        {
            huntTimer = agent.config.searchTime;
        }
        
    }

    public void Exit(AiAgent agent)
    {
    }

    

    
}
