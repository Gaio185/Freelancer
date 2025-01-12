using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class Sounds 
{
    public static void MakeSound(Sound sound, LayerMask enemyLayer, Transform distraction)
    {
        Collider[] listeners = Physics.OverlapSphere(sound.pos, sound.range, enemyLayer);

        LayerMask obstacleMask = LayerMask.GetMask("Wall");

        for (int i = 0; i < listeners.Length; i++)
        {
            Transform listener = listeners[i].transform;
            AiAgent enemy = listener.GetComponent<AiAgent>();

            if (enemy != null)
            {
                Vector3 directionToTarget = (distraction.position - enemy.transform.position).normalized;
                float distanceToTarget = Vector3.Distance(enemy.transform.position, distraction.position);

                if (!Physics.Raycast(enemy.transform.position, directionToTarget, distanceToTarget, obstacleMask) 
                    && enemy.stateMachine.currentState != AiStateId.Hunt
                    && enemy.stateMachine.currentState != AiStateId.Shoot)
                {
                    enemy.distraction = distraction;
                    enemy.stateMachine.ChangeState(AiStateId.Investigate);
                }
            }
        }
    }
}
