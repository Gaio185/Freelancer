using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform[] patrolPoints;
    public ShootBullet shootBullet;
    public Transform distraction;
    [HideInInspector] public Vector3 startingPosition;
    [HideInInspector] public Detection detection;
    public GameObject visor;
    [HideInInspector] public Material visorMaterial;

    // Start is called before the first frame update
    void Start()
    {
        visorMaterial = visor.GetComponent<Renderer>().material;
        startingPosition = transform.position;
        shootBullet = GetComponent<ShootBullet>();
        detection = GetComponent<Detection>();
        navMeshAgent = GetComponent<NavMeshAgent>();    
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterStates(new AIPatrolState());
        stateMachine.RegisterStates(new AIHuntPlayerState());
        stateMachine.RegisterStates(new AIStunnedState());
        stateMachine.RegisterStates(new AIIdleState());
        stateMachine.RegisterStates(new AIInvestigateState());
        stateMachine.RegisterStates(new AIShootState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
