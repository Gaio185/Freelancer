using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sounds 
{
    public static void MakeSound(Sound sound, LayerMask enemyLayer, Transform distraction)
    {
        Collider[] listeners = Physics.OverlapSphere(sound.pos, sound.range, enemyLayer);

        for (int i = 0; i < listeners.Length; i++)
        {
            Transform listener = listeners[i].transform;

            AiAgent enemy = listener.GetComponent<AiAgent>();
            if (enemy != null)
            {
                enemy.distraction = distraction;
                enemy.stateMachine.ChangeState(AiStateId.Investigate);
            }
        }
    }
}
