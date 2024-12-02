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

    [HideInInspector] public Renderer renderer;
    [HideInInspector] public Material material;

    [HideInInspector] public Texture2D greenTexture;
    [HideInInspector] public Texture2D greenEmission;

    [HideInInspector] public Texture2D yellowTexture;
    [HideInInspector] public Texture2D yellowEmission;

    [HideInInspector] public Texture2D redTexture;
    [HideInInspector] public Texture2D redEmission;

    private string greenTexturePath = "Textures/SecurityBotTextures/BotColorGreen";
    private string yellowTexturePath = "Textures/SecurityBotTextures/BotColorYellow";
    private string redTexturePath = "Textures/SecurityBotTextures/BotColorRed";
    private string greenEmissionPath = "Textures/SecurityBotTextures/BotEmissiveGreen";
    private string yellowEmissionPath = "Textures/SecurityBotTextures/BotEmissiveYellow";
    private string redEmissionPath = "Textures/SecurityBotTextures/BotEmissiveRed";


    //public GameObject visor;
    //[HideInInspector] public Material visorMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //Load Textures
        greenTexture = Resources.Load<Texture2D>(greenTexturePath);
        yellowTexture = Resources.Load<Texture2D>(yellowTexturePath);
        redTexture = Resources.Load<Texture2D>(redTexturePath);
        greenEmission = Resources.Load<Texture2D>(greenEmissionPath);
        yellowEmission = Resources.Load<Texture2D>(yellowEmissionPath);
        redEmission = Resources.Load<Texture2D>(redEmissionPath);

        //visorMaterial = visor.GetComponent<Renderer>().material;
        renderer = GetComponent<Renderer>();
        material = renderer.material;
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
