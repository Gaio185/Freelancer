using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIHuntPlayerState : AiState
{
    public Transform playerTransform;
    private float timer = 0.0f;
    private float huntTimer = 0.0f;

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

        agent.detection.playerMovement.isHunted = true;
        agent.detection.visorMaterial.color = Color.red;

        agent.navMeshAgent.isStopped = false;
        huntTimer = agent.config.huntTime;
    }

    public void Update(AiAgent agent)
    {
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(agent.transform.forward), 5 * Time.deltaTime);

        if (!agent.navMeshAgent.enabled) { return; }

        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = playerTransform.position;
        }

        if (agent.detection.canSeePlayer)
        {
            agent.stateMachine.ChangeState(AiStateId.Shoot);
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
                agent.detection.playerDetected = false;
                agent.stateMachine.ChangeState(AiStateId.Investigate);
            }
        }
        else
        {
            huntTimer = agent.config.huntTime;
        }
        
    }

    public void Exit(AiAgent agent)
    {
    }

    

    
}
