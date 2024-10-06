using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    public float radius;
    public float cutoffRadius;
    [Range(0, 360)]
    public float angle;
    [Range(0, 360)]
    public float cutoffAngle;


    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private Quaternion rotationRef;

    public bool canSeePlayer;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        rotationRef = transform.rotation;
        StartCoroutine(DetectionRoutine());
    }

    void Update()
    {
        if(canSeePlayer)
        {
            transform.rotation = Quaternion.Slerp((transform.rotation),Quaternion.LookRotation(playerRef.transform.position - transform.position), 5 * Time.deltaTime);
        }
        else if(tag == "Camera")
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationRef, 5 * Time.deltaTime);
        }
    }

    private IEnumerator DetectionRoutine()
    {

        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            DetectPlayer();
        }
    }

    private void DetectPlayer()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            if(Vector3.Distance(transform.position, playerRef.transform.position) > cutoffRadius || tag != "Camera")
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if ((Vector3.Angle(transform.forward, directionToTarget) < angle / 2) && (Vector3.Angle(transform.position, directionToTarget) < cutoffAngle/2))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        canSeePlayer = true;
                        Debug.Log("Player Detected");
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }  
        }else if(canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}