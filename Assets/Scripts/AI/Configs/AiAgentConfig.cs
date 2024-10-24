using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxDistance = 1.0f;
    public float maxTime = 1.0f;
    public float huntTime = 5.0f;
    public float stunTime = 7.0f;
    public float investigateTime = 10.0f;
    public float investigateInterval = 3.0f;
}
