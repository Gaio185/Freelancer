using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkerAlertState : WorkerState
{
    private Transform agentPositionRef;

    WorkerStateId WorkerState.GetId()
    {
        return WorkerStateId.Alert;
    }

    public void Enter(WorkerAgent agent)
    {
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.destination = agent.enemyRef.transform.position;
    }

    public void Update(WorkerAgent agent)
    {
        if (agent.navMeshAgent.destination != null)
        {
            if(agent.navMeshAgent.remainingDistance <= 2.0f)
            {
                agent.navMeshAgent.ResetPath();

                agent.enemyRef.stateMachine.ChangeState(AiStateId.Investigate);

                agent.stateMachine.ChangeState(WorkerStateId.Wander);
            }
        }
    }

    public void Exit(WorkerAgent agent)
    {
       
    } 
}
