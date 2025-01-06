using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkerAlertState : WorkerState
{
    private bool hasReachedEnemy;
    private bool alertPointReached;

    WorkerStateId WorkerState.GetId()
    {
        return WorkerStateId.Alert;
    }

    public void Enter(WorkerAgent agent)
    {
        agent.animator.SetBool("isWalking", true);
        hasReachedEnemy = false;
        alertPointReached = false;
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = 4.0f;

        agent.navMeshAgent.destination = agent.alertPoint.position;
    }

    public void Update(WorkerAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance <= 2.0f)
        {
            if (agent.navMeshAgent.remainingDistance <= 2.0f && !hasReachedEnemy && alertPointReached)
            {
                agent.animator.SetBool("isWalking", false);
                agent.navMeshAgent.ResetPath();
                agent.detection.player.movement.isHunted = true;
                agent.enemyRef.stateMachine.ChangeState(AiStateId.Investigate);
                hasReachedEnemy = true;
            }
            else if(!alertPointReached) 
            {
                agent.navMeshAgent.destination = agent.enemyRef.transform.position;
                alertPointReached = true;
            }
        }

        if (agent.enemyRef.stateMachine.currentState != AiStateId.Investigate && hasReachedEnemy)
        {
            agent.detection.player.movement.isHunted = false;
            hasReachedEnemy = false;
            alertPointReached = false;
            agent.stateMachine.ChangeState(WorkerStateId.Wander);
        }
    }

    public void Exit(WorkerAgent agent)
    {
    } 
}
