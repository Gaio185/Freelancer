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
        agent.material.SetTexture("_BaseMap", agent.greenTexture);
        agent.material.SetTexture("_EmissionMap", agent.greenEmission);
        agent.material.SetColor("_EmissionColor", Color.white);
        //agent.visorMaterial.color = Color.green;
        agent.navMeshAgent.isStopped = false;
    }

    public void Update(AiAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance < 0.1f)
        {
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, agent.detection.rotationRef, 5 * Time.deltaTime);
        }

        if (agent.detection.canSeePlayer && agent.detection.shouldDetect)
        {
            agent.material.SetTexture("_BaseMap", agent.yellowTexture);
            agent.material.SetTexture("_EmissionMap", agent.yellowEmission);
            //agent.visorMaterial.color = Color.yellow;
            agent.navMeshAgent.isStopped = true; 
        }
        else
        {
            agent.material.SetTexture("_BaseMap", agent.greenTexture);
            agent.material.SetTexture("_EmissionMap", agent.greenEmission);
            //agent.visorMaterial.color = Color.green;
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
