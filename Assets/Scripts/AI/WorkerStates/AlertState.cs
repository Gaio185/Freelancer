using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WorkerAlertState : WorkerState
{
    private LayerMask enemyLayer;
    private float shortestDistance;
    private Transform agentPositionRef;
    [HideInInspector] public Transform enemyToAlert;
    [HideInInspector] public AiAgent enemyRef;

    WorkerStateId WorkerState.GetId()
    {
        return WorkerStateId.Alert;
    }

    public void Enter(WorkerAgent agent)
    {
        enemyRef = new AiAgent();
        agent.navMeshAgent.isStopped = false;
        //agentPositionRef.position = agent.transform.position;

        shortestDistance = 1000.0f;

        enemyLayer = LayerMask.GetMask("Enemy");

        Collider[] enemies = Physics.OverlapSphere(agent.transform.position, 300, enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            Transform enemyTransform = enemies[i].transform;
            float distanceToEnemy = Vector3.Distance(agent.transform.position, enemyTransform.position);
            if (distanceToEnemy < shortestDistance)
            {
                enemyToAlert = enemyTransform;
                shortestDistance = distanceToEnemy;
                Debug.Log(shortestDistance);
            }
        }

        enemyRef = enemyToAlert.GetComponent<AiAgent>();

        if(enemyToAlert != null)
        {
            agent.navMeshAgent.destination = enemyToAlert.position;
        }
        
    }

    public void Update(WorkerAgent agent)
    {

        //enemy.distraction = agentPositionRef;

        if (agent.navMeshAgent.destination != null && enemyRef.stateMachine.currentState != AiStateId.Investigate)
        {
            if(agent.navMeshAgent.remainingDistance <= 2.0f)
            {
                agent.navMeshAgent.ResetPath();

                enemyRef.stateMachine.ChangeState(AiStateId.Investigate);

                if (enemyRef.stateMachine.currentState != AiStateId.Investigate)
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
