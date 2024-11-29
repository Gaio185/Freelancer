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
    public GameObject visor;
    [HideInInspector] public Material visorMaterial;
    [HideInInspector] public Renderer renderer;
    [HideInInspector] public Material material;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        visorMaterial = visor.GetComponent<Renderer>().material;
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
