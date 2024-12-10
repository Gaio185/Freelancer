using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyShootStateNode : ConditionNode
{
    public AiStateId enemyState;
    public Blackboard blackboard;

    protected override void OnStart()
    {
        enemyState = blackboard.agent.stateMachine.currentState;
        if (enemyState == AiStateId.Shoot)
        {
            condition = true;
        }
        else
        {
            condition = false;
        }
    }
}
