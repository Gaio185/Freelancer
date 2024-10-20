using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TOAgent : MonoBehaviour
{

    public TerminalObjectStateMachine stateMachine;
    public TOStateId initialState;
    public TerminalObjectConfig config;
    [HideInInspector] public Detection detection;

    void Start()
    {
        detection = GetComponent<Detection>();
        stateMachine = new TerminalObjectStateMachine(this);
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
