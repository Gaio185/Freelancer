using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class SelectorBT : NodeBT
    {
        public SelectorBT() : base() { }
        public SelectorBT(List<NodeBT> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach(NodeBT node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}