using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : WorkerState
{
    private float timer;

    public WorkerStateId GetId()
    {
        return WorkerStateId.Wander;
    }

    public void Enter(WorkerAgent agent)
    {
        timer = 0.0f;
    }

    public void Update(WorkerAgent agent)
    {
        if (!agent.navMeshAgent.hasPath)
        {
            timer -= Time.deltaTime;

            if(timer <= 0.0f)
            {
                WorldBounds bounds = GameObject.FindObjectOfType<WorldBounds>();
                Vector3 min = bounds.minBounds.position;
                Vector3 max = bounds.maxBounds.position;

                Vector3 randomPosition = new Vector3(
                    Random.Range(min.x, max.x),
                    Random.Range(min.y, max.y),
                    Random.Range(min.z, max.z));

                agent.navMeshAgent.destination = randomPosition;
                timer = agent.config.wanderInterval;
                agent.animator.SetBool("isWalking", true);
            }
        }else if (agent.navMeshAgent.remainingDistance <= 0.5f)
        {
            agent.animator.SetBool("isWalking", false);
            agent.navMeshAgent.ResetPath();
        }
        else if (agent.detection.canSeePlayer && agent.detection.shouldDetect)
        {
            agent.animator.SetBool("isWalking", false);
            agent.navMeshAgent.ResetPath();
            agent.navMeshAgent.isStopped = true;
        }
        else
        {
            agent.navMeshAgent.isStopped = false;
        }

        if (agent.detection.playerDetected && agent.enemyRef.stateMachine.currentState != AiStateId.Investigate)
        {
            agent.navMeshAgent.ResetPath();
            agent.stateMachine.ChangeState(WorkerStateId.Alert);
        }
    }

    public void Exit(WorkerAgent agent)
    {
        
    }
}
