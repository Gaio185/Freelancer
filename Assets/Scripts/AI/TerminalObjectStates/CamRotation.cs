using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    private Quaternion rotationRef;
    private Quaternion initialRotation;
    private float timer = 0f;
    private bool startNextRotation = true;
    private bool undoRotation = false;

    public bool rotateRight;
    public float yaw;
    public float rotationDuration;
    public float rotationDelay;

    void Start()
    {
        //SetupStartRotation();
    }

    private void Update()
    {
        if (startNextRotation && rotateRight)
        {
            StartCoroutine(Rotate(yaw, rotationDuration));
        }
        else if (startNextRotation && !rotateRight)
        {
            StartCoroutine(Rotate(-yaw, rotationDuration));
        }
    }

    IEnumerator Rotate(float yaw, float duration)
    {
        startNextRotation = false;

        rotationRef = transform.rotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = rotationRef * Quaternion.AngleAxis(timer / duration * (yaw / 2), Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(rotationDelay);

        timer = 0f;
        rotationRef = transform.rotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = rotationRef * Quaternion.AngleAxis(timer / duration * (-yaw / 2), Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(rotationDelay);

        timer = 0f;
        startNextRotation = true;
        rotateRight = !rotateRight;
        
    }

    private void SetupStartRotation()
    {
        if (rotateRight)
        {
            transform.localRotation = Quaternion.AngleAxis(-yaw / 2, Vector3.up);
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(yaw / 2, Vector3.up);
        }
    }
}
