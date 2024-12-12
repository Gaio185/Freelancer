using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : DecoratorNode
{
    public bool condition;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (condition && child != null)
        {
            return child.Update();
        }
        return State.Failure;
    }
}
