using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class TreeBT : MonoBehaviour
    {
        private NodeBT _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if(_root != null)
            {
                _root.Evaluate();
            }
        }

        protected abstract NodeBT SetupTree();
    }
}
