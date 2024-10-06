using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisibility : MonoBehaviour
{
    public Transform playerCamera; // Assign the player's camera in the Inspector
    public LayerMask wallLayer; // Assign the wall layer in the Inspector (to detect walls)
    private Renderer objectRenderer;

    void Start()
    {
        // Get the object's renderer to enable/disable visibility
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        CheckVisibility();
    }

    void CheckVisibility()
    {
        // Perform a raycast from the camera to the object
        Ray ray = new Ray(playerCamera.position, (transform.position - playerCamera.position));
        RaycastHit hit;

        // Check if the ray hits anything
        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits this object directly, show the object
            if (hit.transform == transform)
            {
                objectRenderer.enabled = true;
            }
            // If the ray hits a wall or something else, hide the object
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                objectRenderer.enabled = false;
            }
        }
    }
}