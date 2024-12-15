using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOShootState : TOState
{
    private float timer;

    public TOStateId GetId()
    {
        return TOStateId.Shoot;
    }

    public void Enter(TOAgent agent)
    {
        Debug.Log("Shoot");
        agent.shootBullet.Shoot();
        timer = agent.config.shootInterval;
        agent.material.SetTexture("_EmissionMap", agent.redEmission);
        agent.material.SetColor("_EmissionColor", Color.red);
        agent.detection.lightRef.color = Color.red;
    }

    public void Update(TOAgent agent)
    {
        agent.material.SetTexture("_EmissionMap", agent.redEmission);
        agent.material.SetColor("_EmissionColor", Color.red);
        agent.detection.lightRef.color = Color.red;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (agent.detection.canSeePlayer)
            {
                agent.shootBullet.Shoot();
                timer = agent.config.shootInterval;
            }
        }

        if (!agent.detection.canSeePlayer)
        {
            agent.stateMachine.ChangeState(TOStateId.On);
        }
    }

    public void Exit(TOAgent agent)
    {

    }
}
