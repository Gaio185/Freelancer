using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.AI.Navigation;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public float detectionTimer;
    public float radius;
    public float awarenessRadius;
    public float cutoffRadius;
    [Range(0, 360)]
    public float angle;
    [Range(0, 360)]
    public float cutoffAngle;

    public GameObject playerRef;
    public Player player;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector] public Quaternion rotationRef;
    [HideInInspector] public Vector3 forwardRef;
    private float timer;

    [HideInInspector] public bool canSeePlayer;
    [HideInInspector] public bool shouldDetect;
    [HideInInspector] public bool playerDetected;

    public Light lightRef;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        player = playerRef.GetComponent<Player>();
        rotationRef = transform.rotation;
        forwardRef = transform.forward;
        StartCoroutine(DetectionRoutine());
        timer = detectionTimer;
    }

    void Update()
    {
        if (canSeePlayer && tag == "Sentry")
        {
            transform.rotation = Quaternion.Slerp((transform.rotation), Quaternion.LookRotation(playerRef.transform.position - transform.position), 5 * Time.deltaTime);
        }
        else if (tag == "Sentry")
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forwardRef), 5 * Time.deltaTime);
        }

        shouldDetect = ShouldBeDetected();

        if (canSeePlayer && !playerDetected && shouldDetect)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = detectionTimer;
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
            Transform target = rangeChecks[0].transform;

            if (Vector3.Distance(transform.position, target.transform.position) > cutoffRadius || (tag != "Camera" && tag != "Sentry"))
            {
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if ((Vector3.Angle(transform.forward, directionToTarget) < angle / 2) && (Vector3.Angle(forwardRef, directionToTarget) < cutoffAngle /2))

                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        canSeePlayer = true;
                        if(timer <= 0.0f)
                        {
                            playerDetected = true;
                        }
                        else { playerDetected = false; }
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

    private bool ShouldBeDetected()
    {
        if (!player.movement.isHunted)
        {
            if (!player.movement.hasClearance || player.switchWeapon.hasWeaponEquipped)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
