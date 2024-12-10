using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BT;

public class TaskGoToTarget : NodeBT
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if(Vector3.Distance(_transform.position, target.position) > 0.5f && Vector3.Distance(_transform.position, target.position) < 6)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, GuardBT.speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }
        else
        {
            ClearData("target");
        }

        state = NodeState.RUNNING;
        return state;
    }
}
