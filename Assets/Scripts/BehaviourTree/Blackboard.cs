using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard
{
    //public Vector3 moveToPosition;
    //public GameObject moveToObject;
    public AiAgent agent;
    public GameObject enemy;

    public void Start()
    {
        agent = enemy.GetComponent<AiAgent>();
    }
}
