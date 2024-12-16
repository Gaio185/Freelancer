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

        if (agent.detection.canSeePlayer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                agent.shootBullet.Shoot();
                timer = agent.config.shootInterval;
            }
        }
        else
        {
            agent.detection.playerDetected = false;
            agent.stateMachine.ChangeState(TOStateId.On);
        }
    }

    public void Exit(TOAgent agent)
    {

    }
}
