using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BT;

public class CheckEnemyInFOVRange : NodeBT
{
    private static int _enemyBTLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, GuardBT.fovRange, _enemyBTLayerMask);

            if(colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
