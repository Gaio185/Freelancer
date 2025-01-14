using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        agent.detection.player.movement.isHunted = false;
        agent.navMeshAgent.isStopped = true;
        timer = agent.config.stunTime;
        agent.material.SetTexture("_BaseMap", agent.stunnedTexture);
        agent.material.SetColor("_EmissionColor", Color.black);
        agent.audioSource.clip = agent.enemyStunned;
        agent.audioSource.Play();
    }

    public void Update(AiAgent agent)
    {
        agent.detection.canSeePlayer = false;
        agent.detection.playerDetected = false;
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
