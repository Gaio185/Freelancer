using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    private Quaternion initialRotation;
    private float timer = 0f;
    private bool startNextRotation = true;

    public bool rotateRight;
    public float yaw;
    public float rotationDuration;

    void Start()
    {
        StartCoroutine(Rotate(yaw, rotationDuration));
    }

    private void Update()
    {
        if(startNextRotation && rotateRight)
        {
            StartCoroutine(Rotate(yaw, rotationDuration));
        }else if(startNextRotation && !rotateRight)
        {
            StartCoroutine(Rotate(-yaw, rotationDuration));
        }
    }

    IEnumerator Rotate(float yaw, float duration)
    {
        startNextRotation = false;

        initialRotation = transform.rotation;

        while (timer <= 0f)
        {
            timer += Time.deltaTime;
            transform.rotation = initialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.up);
            yield return null;
        }

        startNextRotation = true;
        rotateRight = ! rotateRight;
    }
}
