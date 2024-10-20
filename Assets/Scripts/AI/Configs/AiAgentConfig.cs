using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxDistance = 1.0f;
    public float maxTime = 1.0f;
    public float searchTime = 5.0f;
}
