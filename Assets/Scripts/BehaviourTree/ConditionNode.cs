using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : DecoratorNode
{
    public Func<bool> condition;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (condition != null && condition() && child != null)
        {
            return child.Update();
        }
        return State.Failure;
    }
}
