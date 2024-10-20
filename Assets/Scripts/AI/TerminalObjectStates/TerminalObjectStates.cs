using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOStateId
{
    On,
    Off,
    Alert,
    Shoot
}

public interface TOState
{
    TOStateId GetId();
    void Enter(TOAgent agent);
    void Update(TOAgent agent);
    void Exit(TOAgent agent);
}
