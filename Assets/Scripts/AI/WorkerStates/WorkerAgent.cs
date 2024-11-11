using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAgent : MonoBehaviour
{
    public WorkerStateMachine stateMachine;
    public WorkerStateId initialState;
    public NavMeshAgent navMeshAgent;
    public WorkerAgentConfig config;
    public AiAgent enemyRef;
    [HideInInspector] public Vector3 startingPosition;
    [HideInInspector] public Detection detection;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        detection = GetComponent<Detection>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new WorkerStateMachine(this);
        stateMachine.RegisterStates(new WanderState());
        stateMachine.RegisterStates(new WorkerAlertState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
