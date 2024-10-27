using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AIShootState : AiState
{
    private float timer;

    public AiStateId GetId()
    {
        return AiStateId.Shoot;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Shoot");
        agent.navMeshAgent.isStopped = true;
        agent.detection.isMoving = false;
        agent.shootBullet.Shoot();
        timer = agent.config.shootInterval;
    }

    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (agent.detection.canSeePlayer)
            {
                agent.shootBullet.Shoot();
                timer = agent.config.shootInterval;
            }
        }
        
        if(!agent.detection.canSeePlayer)
        {
            agent.stateMachine.ChangeState(AiStateId.Hunt);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }

}
