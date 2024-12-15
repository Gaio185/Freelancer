using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TOAgent : MonoBehaviour
{

    public TerminalObjectStateMachine stateMachine;
    public TOStateId initialState;
    public TerminalObjectConfig config;
    public AiAgent[] agents;
    public ShootBullet shootBullet;
    [HideInInspector] public Detection detection;
    [HideInInspector] public Renderer renderer;
    [HideInInspector] public Material material;

    [HideInInspector] public Texture2D greenEmission;
    [HideInInspector] public Texture2D yellowEmission;
    [HideInInspector] public Texture2D redEmission;

    private string greenEmissionPath = "Textures/TurretTextures/TurretEmissionGreen";
    private string yellowEmissionPath = "Textures/TurretTextures/TurretEmissionYellow";
    private string redEmissionPath = "Textures/TurretTextures/TurretEmissionRed";

    void Start()
    {
        greenEmission = Resources.Load<Texture2D>(greenEmissionPath);
        yellowEmission = Resources.Load<Texture2D>(yellowEmissionPath);
        redEmission = Resources.Load<Texture2D>(redEmissionPath);

        renderer = GetComponent<Renderer>();
        material = renderer.material;
        detection = GetComponent<Detection>();
        shootBullet = GetComponent<ShootBullet>();
        stateMachine = new TerminalObjectStateMachine(this);
        stateMachine.RegisterStates(new OnState());
        stateMachine.RegisterStates(new OffState());
        stateMachine.RegisterStates(new AlertState());
        stateMachine.RegisterStates(new TOShootState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
