using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIInvestigateState : AiState
{
    public Transform playerTransform;
    public Vector3 normalizedPosition;
    public float timer;
    public float interval;

    public AiStateId GetId()
    {
        return AiStateId.Investigate;
    }

    public void Enter(AiAgent agent)
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        //agent.detection.isMoving = true;
        if (!agent.distraction)
        {
            agent.navMeshAgent.destination = playerTransform.position;
        }
        else
        {
            agent.navMeshAgent.destination = agent.distraction.position;
        }

        agent.visorMaterial.color = Color.yellow;

        timer = agent.config.investigateTime;
        interval = 0;
    }

    public void Update(AiAgent agent)
    {
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(agent.transform.forward), 5 * Time.deltaTime);
        if (!agent.detection.canSeePlayer || agent.detection.canSeePlayer && !agent.detection.shouldDetect)
        {
            timer -= Time.deltaTime;
            interval -= Time.deltaTime;
            agent.navMeshAgent.isStopped = false;

            Vector3 randomPosition;
           
            if (!agent.navMeshAgent.hasPath && interval <= 0 && RandomPoint(agent.transform.position, agent.detection.awarenessRadius, out randomPosition))
            {
                Vector3 directionToTarget = (randomPosition - agent.transform.position).normalized;
                float distanceToTarget = Vector3.Distance(agent.transform.position, randomPosition);

                if (!Physics.Raycast(agent.transform.position, directionToTarget, distanceToTarget, agent.detection.obstacleMask))
                {
                    agent.visorMaterial.color = Color.yellow;
                    randomPosition.y = agent.transform.position.y;
                    agent.navMeshAgent.destination = randomPosition;
                    interval = agent.config.investigateInterval;
                }
                
            }

            if(timer <= 0)
            {
                agent.distraction = null;
                agent.detection.player.movement.isHunted = false;
                agent.stateMachine.ChangeState(agent.initialState);
            }

        }else if(agent.detection.canSeePlayer && agent.detection.shouldDetect) 
        {
            agent.navMeshAgent.isStopped = true;
            timer = agent.config.investigateTime;
        }

        if (agent.detection.playerDetected)
        {
            agent.distraction = null;
            agent.stateMachine.ChangeState(AiStateId.Hunt);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }

    private bool RandomPoint(Vector3 center, float radius, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
