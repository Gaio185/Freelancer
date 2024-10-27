using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AIShootState : AiState
{
    private ObjectPool<Bullet> bulletPool;
    private Bullet bullet;

    public AiStateId GetId()
    {
        return AiStateId.Shoot;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Shoot");
        agent.navMeshAgent.isStopped = true;
        agent.detection.isMoving = false;
        //Shoot(agent);
    }

    public void Update(AiAgent agent)
    {
        bullet.fireRate -= Time.deltaTime;
        if(agent.detection.canSeePlayer)
        {
            //Shoot(agent);
        }
        else
        {
            agent.stateMachine.ChangeState(AiStateId.Hunt);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }

}
