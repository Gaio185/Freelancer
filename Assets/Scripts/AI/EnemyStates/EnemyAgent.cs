using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public List<GameObject> wheels;
    public AIStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform[] patrolPoints;
    public ShootBullet shootBullet;
    public Transform distraction;
    public AiAgent[] aiAgents;

    public Floor initialFloor;

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

    [HideInInspector] public Texture2D stunnedTexture;

    private string greenTexturePath = "Textures/SecurityBotTextures/BotColorGreen";
    private string yellowTexturePath = "Textures/SecurityBotTextures/BotColorYellow";
    private string redTexturePath = "Textures/SecurityBotTextures/BotColorRed";
    private string greenEmissionPath = "Textures/SecurityBotTextures/BotEmissiveGreen";
    private string yellowEmissionPath = "Textures/SecurityBotTextures/BotEmissiveYellow";
    private string redEmissionPath = "Textures/SecurityBotTextures/BotEmissiveRed";
    private string stunnedTexturePath = "Textures/SecurityBotTextures/BotColorStunned";

    [HideInInspector] public float timeElapsed;
    private Color emissionColor;

    public AudioSource audioSource;
    public AudioClip enemyStunned;
    [SerializeField] private AudioClip enemyMoving;

    void Start()
    {
        //Load Textures
        greenTexture = Resources.Load<Texture2D>(greenTexturePath);
        yellowTexture = Resources.Load<Texture2D>(yellowTexturePath);
        redTexture = Resources.Load<Texture2D>(redTexturePath);
        greenEmission = Resources.Load<Texture2D>(greenEmissionPath);
        yellowEmission = Resources.Load<Texture2D>(yellowEmissionPath);
        redEmission = Resources.Load<Texture2D>(redEmissionPath);
        stunnedTexture = Resources.Load<Texture2D>(stunnedTexturePath);

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

    void Update()
    {
        stateMachine.Update();

        for (int i = 0; i < wheels.Count; i++)
        {
            if (navMeshAgent.isStopped || !navMeshAgent.hasPath)
            {
                wheels[i].GetComponent<Animator>().SetBool("isMoving", false);
                if(audioSource.isPlaying && audioSource.clip == enemyMoving)
                {
                    audioSource.Stop();
                    audioSource.loop = false;
                }
                
            }
            else
            {
                wheels[i].GetComponent<Animator>().SetBool("isMoving", true);
                if (!audioSource.isPlaying)
                {
                    audioSource.loop = true;
                    audioSource.clip = enemyMoving;
                    audioSource.volume = 0.15f;
                    audioSource.Play();
                }
                
            }
        }
    }

    public void UpdateEmissionColor(Color startColor, Color endColor)
    {
        timeElapsed += Time.deltaTime;

        float t = Mathf.Clamp01(timeElapsed / detection.detectionTimer);

        emissionColor = Color.Lerp(Color.yellow, Color.red, t);
        material.SetColor("_EmissionColor", emissionColor);
    }
}
