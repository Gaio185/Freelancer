using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 3f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // Camera movement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E)) moveY += moveSpeed * Time.deltaTime; // Up
        if (Input.GetKey(KeyCode.Q)) moveY -= moveSpeed * Time.deltaTime; // Down

        transform.Translate(new Vector3(moveX, moveY, moveZ));

        // Camera rotation
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }
}
