using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStunnedState : AiState
{
    public float timer = 0.0f;

    public AiStateId GetId()
    {
        return AiStateId.Stunned;
    }

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        timer = agent.config.stunTime;
    }

    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            agent.stateMachine.ChangeState(agent.initialState);
        }
    }

    public void Exit(AiAgent agent)
    {
    }

}
