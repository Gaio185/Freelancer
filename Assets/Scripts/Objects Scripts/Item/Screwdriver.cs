using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    public float interactionDistance = 2f; // Distance to interact with the vent door
    public LayerMask ventLayer; // LayerMask to specify the vent door layer

    void Update()
    {
        // Check if the player clicks the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Check for the nearest vent door
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance, ventLayer);

            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    // Check if the object we hit is the vent door
                    VentDoor ventDoor = hitCollider.GetComponent<VentDoor>();
                    if (ventDoor != null)
                    {
                        ventDoor.DestroyVentDoor();
                    }
                }
            }
        }
    }
}
