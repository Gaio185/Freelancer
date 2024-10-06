using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    void Start()
    {
        targetPoint = 0;
    }


    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            targetPoint++;
            if (targetPoint >= patrolPoints.Length)
            {
                targetPoint = 0;
            }
        }
        MoveToTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(patrolPoints[targetPoint].position - transform.position), 5 * Time.deltaTime);
    }
}
