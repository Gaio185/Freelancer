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
        timer = agent.config.shootInterval;
        agent.detection.player.movement.isHunted = true;
    }

    public void Update(AiAgent agent)
    {
        agent.transform.rotation = Quaternion.Slerp((agent.transform.rotation),
                    Quaternion.LookRotation(agent.detection.playerRef.transform.position - agent.transform.position),
                    5 * Time.deltaTime);

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
