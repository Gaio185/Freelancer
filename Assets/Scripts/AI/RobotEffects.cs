using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEffects : MonoBehaviour
{
    public ParticleSystem shockEffect;
    public ParticleSystem sparksEffect;
    public ParticleSystem smokeEffect;

    private AiAgent agent;
    private AiStateId currentState;

    private void Start()
    {
        agent = GetComponent<AiAgent>();

        shockEffect.gameObject.SetActive(false);
        sparksEffect.gameObject.SetActive(false);
        smokeEffect.gameObject.SetActive(false);

    }

    private void Update()
    {
        currentState = agent.stateMachine.currentState;

        if(currentState == AiStateId.Stunned)
        {
            PlayStunEffects();
        }
        else
        {
            StopStunEffects();
        }
    }

    // Método para ativar os efeitos de stun
    public void PlayStunEffects()
    {
        shockEffect.gameObject.SetActive(true);
        sparksEffect.gameObject.SetActive(true);
        smokeEffect.gameObject.SetActive(true);
    }

    // Método para parar os efeitos
    public void StopStunEffects()
    {
        shockEffect.gameObject.SetActive(false);
        sparksEffect.gameObject.SetActive(false);
        smokeEffect.gameObject.SetActive(false);
    }
}
