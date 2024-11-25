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

    private Animator animator; // Reference to the Animator component
    private float idleThreshold = 0.1f; // Threshold for detecting idle (if velocity is small)

    // Start is called before the first frame update
    void Start()
    {
        // Save starting position
        startingPosition = transform.position;

        // Initialize components
        detection = GetComponent<Detection>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // Automatically find Animator in child object

        if (animator == null)
        {
            Debug.LogError("No Animator found in child objects. Ensure the character model has an Animator component.");
        }

        // Set up state machine
        stateMachine = new WorkerStateMachine(this);
        stateMachine.RegisterStates(new WanderState());
        stateMachine.RegisterStates(new WorkerAlertState());
        stateMachine.ChangeState(initialState);

        // Adjust NavMeshAgent settings for smooth movement
        ConfigureNavMeshAgent();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the state machine
        stateMachine.Update();

        // Handle movement and update animations
        UpdateMovement();
    }

    // Handles movement logic
    private void UpdateMovement()
    {
        // Check if the agent is moving (based on the NavMeshAgent velocity)
        bool isMoving = navMeshAgent.velocity.sqrMagnitude > idleThreshold;

        // Ensure the agent stops rotating
        if (isMoving)
        {
            // Make sure agent is not spinning uncontrollably
            navMeshAgent.updateRotation = true;
        }
        else
        {
            // Stop rotation when idle to prevent spinning
            navMeshAgent.updateRotation = false;
        }

        // Check if agent's destination is reached
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            navMeshAgent.isStopped = true;  // Stop agent when it reaches the destination
        }
        else
        {
            navMeshAgent.isStopped = false;  // Continue movement if not at destination
        }
    }

    // Adjust the NavMeshAgent for smoother movement
    private void ConfigureNavMeshAgent()
    {
        // Adjust agent size and stopping distance
        navMeshAgent.radius = 0.5f; // Adjust radius to match your model
        navMeshAgent.height = 2.0f; // Set agent height based on your model size
        navMeshAgent.stoppingDistance = 0.5f; // Stop when within this distance of the target

        // Avoid obstacles and prevent weird rotations
        navMeshAgent.avoidancePriority = 50;
    }
}
