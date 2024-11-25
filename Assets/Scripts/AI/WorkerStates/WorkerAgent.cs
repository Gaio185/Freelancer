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

    public Animator animator; // Reference to the Animator component
    private float idleThreshold = 0.1f; // Threshold for detecting idle (if velocity is small)

    // Start is called before the first frame update
    void Start()
    {
        // Save starting position
        startingPosition = transform.position;

        // Initialize components
        detection = GetComponent<Detection>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 

        if (animator == null)
        {
            Debug.LogError("No Animator found in child objects. Ensure the character model has an Animator component.");
        }

        // Set up state machine
        stateMachine = new WorkerStateMachine(this);
        stateMachine.RegisterStates(new WanderState());
        stateMachine.RegisterStates(new WorkerAlertState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the state machine
        stateMachine.Update();
    }
}
