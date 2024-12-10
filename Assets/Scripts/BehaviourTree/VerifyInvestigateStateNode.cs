using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyInvestigateStateNode : ConditionNode
{
    public AiStateId enemyState;
    public Blackboard blackboard;

    protected override void OnStart()
    {
        enemyState = blackboard.agent.stateMachine.currentState;
        if (enemyState == AiStateId.Investigate)
        {
            condition = true;
        }
        else
        {
            condition = false;
        }
    }
}
