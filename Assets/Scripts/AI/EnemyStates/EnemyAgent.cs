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
    public Bullet bulletPrefab;
    [HideInInspector] public Vector3 startingPosition;
    [HideInInspector] public Detection detection;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
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
