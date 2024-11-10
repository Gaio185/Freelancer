using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkerAlertState : WorkerState
{
    private LayerMask enemyLayer;
    private float shortestDistance;
    private AiAgent enemy;
    private Transform distraction;

    WorkerStateId WorkerState.GetId()
    {
        return WorkerStateId.Alert;
    }

    public void Enter(WorkerAgent agent)
    {
        shortestDistance = 1000.0f;

        enemyLayer = LayerMask.GetMask("Enemy");

        Collider[] enemies = Physics.OverlapSphere(agent.transform.position, 300, enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            Transform enemyTransform = enemies[i].transform;
            float distanceToEnemy = Vector3.Distance(agent.transform.position, enemyTransform.position);
            if (distanceToEnemy < shortestDistance)
            {
                enemy = enemyTransform.GetComponent<AiAgent>();
                shortestDistance = distanceToEnemy;
            }
        }

        if(enemy != null)
        {
            agent.navMeshAgent.destination = enemy.transform.position;
        }
        
    }

    public void Update(WorkerAgent agent)
    {
        if (agent.navMeshAgent.hasPath)
        {
            if(agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
            {
                enemy.distraction = agent.transform;
                enemy.stateMachine.ChangeState(AiStateId.Investigate);

                if(enemy.stateMachine.currentState != AiStateId.Investigate)
                {
                    agent.stateMachine.ChangeState(WorkerStateId.Wander);
                }
            }
        }
    }

    public void Exit(WorkerAgent agent)
    {
       
    } 
}
