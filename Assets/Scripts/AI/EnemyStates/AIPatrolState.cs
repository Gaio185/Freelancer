using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolState : AiState
{
    private int targetPoint;
    private NavMeshPath path = new NavMeshPath();
    private Color emissionColor;
    private float timeElapsed;

    public AiStateId GetId()
    {
       return AiStateId.Patrol;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Patrol");
        agent.navMeshAgent.isStopped = false;
        agent.material.SetTexture("_BaseMap", agent.greenTexture);
        agent.material.SetTexture("_EmissionMap", agent.greenEmission);
        agent.material.SetColor("_EmissionColor", Color.green);
        targetPoint = 0;
        agent.timeElapsed = 0;

        agent.navMeshAgent.CalculatePath(agent.patrolPoints[targetPoint].position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.navMeshAgent.destination = agent.patrolPoints[targetPoint].position;
        }else 
        {
            NavMeshHit hit;

            if(NavMesh.SamplePosition(agent.patrolPoints[targetPoint].position, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.navMeshAgent.destination = hit.position;
            }
            else
            {
                //agent.navMeshAgent.destination = agent.returnPoint.position;
            }
        }
        
        
    }

    public void Update(AiAgent agent)
    {
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, 
            Quaternion.LookRotation(agent.transform.forward), 
            5 * Time.deltaTime);

        if (agent.navMeshAgent.remainingDistance <= 0.1f)
        {
            targetPoint++;
            if (targetPoint >= agent.patrolPoints.Length)
            {
                targetPoint = 0;
            }
        }

        agent.navMeshAgent.destination = agent.patrolPoints[targetPoint].position;

        if (agent.detection.canSeePlayer && agent.detection.shouldDetect)
        {
            agent.navMeshAgent.isStopped = true;
            agent.material.SetTexture("_BaseMap", agent.yellowTexture);
            agent.material.SetTexture("_EmissionMap", agent.yellowEmission);
            agent.UpdateEmissionColor(Color.yellow, Color.red);
        }
        else
        {
            agent.timeElapsed = 0;
            agent.navMeshAgent.isStopped = false;
            agent.material.SetTexture("_BaseMap", agent.greenTexture);
            agent.material.SetTexture("_EmissionMap", agent.greenEmission);
            agent.material.SetColor("_EmissionColor", Color.green);
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
