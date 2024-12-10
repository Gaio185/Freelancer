using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class NodeBT
    {
        protected NodeState state;

        public NodeBT parent;
        protected List<NodeBT> children = new List<NodeBT>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public NodeBT()
        {
            parent = null;
        }
        public NodeBT(List<NodeBT> children)
        {
            foreach (NodeBT child in children)
            {
                _Attach(child);
            }
        }

        private void _Attach(NodeBT node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if( _dataContext.TryGetValue(key, out value))
            {
                return value;
            }
            NodeBT node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if(value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if(_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            NodeBT node = parent;
            while(node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }
    }
}
