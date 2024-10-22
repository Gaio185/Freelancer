using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : TOState
{
    public TOStateId GetId()
    {
        return TOStateId.Shoot;
    }

    public void Enter(TOAgent agent)
    {
        Debug.Log("Is Shooting");
    }

    public void Update(TOAgent agent)
    {
        if(!agent.detection.canSeePlayer) agent.stateMachine.ChangeState(TOStateId.On);
    }

    public void Exit(TOAgent agent)
    {
      
    }
}
