using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lean : MonoBehaviour
{
    Quaternion initialRotation;
    public float amt, slerpAmt;
    private float targetZ;

    void Start()
    {
        initialRotation = transform.localRotation;  // Save the initial rotation
        targetZ = initialRotation.eulerAngles.z;    // Set initial target z-axis rotation
    }

    void Update()
    {
        // Get the current Euler angles (yaw, pitch, and roll)
        Vector3 currentEulerAngles = transform.localEulerAngles;

        // Determine target z-axis based on input
        if (Input.GetKey(KeyCode.Q))
        {
            // Lean left: increase the z-axis rotation gradually
            targetZ = initialRotation.eulerAngles.z + amt;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Lean right: decrease the z-axis rotation gradually
            targetZ = initialRotation.eulerAngles.z - amt;
        }
        else
        {
            // No input: smoothly return to the initial z-axis rotation
            targetZ = initialRotation.eulerAngles.z;
        }

        // Smoothly interpolate the z-axis rotation
        currentEulerAngles.z = Mathf.LerpAngle(currentEulerAngles.z, targetZ, Time.deltaTime * slerpAmt);

        // Apply the updated rotation back to the transform
        transform.localRotation = Quaternion.Euler(currentEulerAngles);
    }
}
