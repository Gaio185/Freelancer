using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkerAlertState : WorkerState
{
    private bool hasReachedEnemy;

    WorkerStateId WorkerState.GetId()
    {
        return WorkerStateId.Alert;
    }

    public void Enter(WorkerAgent agent)
    {
        agent.animator.SetBool("isWalking", true);
        hasReachedEnemy = false;
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.destination = agent.enemyRef.transform.position;
    }

    public void Update(WorkerAgent agent)
    {
        if (agent.navMeshAgent.destination != null)
        {
            if(agent.navMeshAgent.remainingDistance <= 2.0f && !hasReachedEnemy)
            {
                agent.animator.SetBool("isWalking", false);
                agent.navMeshAgent.ResetPath();
                agent.detection.player.movement.isHunted = true;
                agent.enemyRef.stateMachine.ChangeState(AiStateId.Investigate); 
                hasReachedEnemy = true;
            }

            if (agent.enemyRef.stateMachine.currentState != AiStateId.Investigate && hasReachedEnemy)
            {
                agent.detection.player.movement.isHunted = false;
                agent.stateMachine.ChangeState(WorkerStateId.Wander);
            }
        }
    }

    public void Exit(WorkerAgent agent)
    {
       
    } 
}
