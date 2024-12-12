using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BT;

public class GuardBT : TreeBT
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 6f;

    protected override NodeBT SetupTree()
    {
        NodeBT root = new SelectorBT(new List<NodeBT>
        {
            new SequenceBT(new List<NodeBT>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform)
            }),
            new TaskPatrolBT(transform, waypoints)
        });

        return root;
    }
}
