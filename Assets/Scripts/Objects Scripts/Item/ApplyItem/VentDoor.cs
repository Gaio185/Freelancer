using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoor : MonoBehaviour
{
    public GameObject ventObject;  // Reference to the vent GameObject (the one to be deactivated)
    public Transform destination;  // Destination for teleportation (can be an empty GameObject)
    public float interactionDistance = 5f; // Distance to interact with the vent

    public GameObject player; // Manually assign the player here

    private MeshRenderer ventRenderer; // MeshRenderer to deactivate
    private bool ventIsOpen = false; // Track whether the vent is opened or not

    // Initialize the MeshRenderer reference
    private void Awake()
    {
        ventRenderer = ventObject.GetComponent<MeshRenderer>();
    }

    // Method to open the vent (called when screwdriver is used)
    public void OpenVent()
    {
        if (!ventIsOpen)
        {
            ventIsOpen = true;
            DeactivateVentMesh(); // Deactivate the MeshRenderer
        }
    }

    // Method to deactivate the vent mesh (making it disappear visually)
    private void DeactivateVentMesh()
    {
        if (ventRenderer != null)
        {
            ventRenderer.enabled = false; // Hide the vent object
            Debug.Log("Vent mesh has been deactivated.");
        }
        else
        {
            Debug.LogError("MeshRenderer is missing on ventObject.");
        }
    }

    // Method to handle teleportation when 'F' is pressed
    public void EnterVent()
    {
        if (player != null && destination != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // Check if player is close enough to the vent to interact
            if (distance <= interactionDistance)
            {
                // Check if player is already at the destination to prevent unnecessary teleport
                if (Vector3.Distance(player.transform.position, destination.position) < 0.1f)
                {
                    Debug.Log("Player is already at the destination, no teleport needed.");
                    return;
                }

                Debug.Log("Teleporting player to destination.");

                // Temporarily disable CharacterController or Rigidbody if present
                CharacterController characterController = player.GetComponent<CharacterController>();
                Rigidbody rb = player.GetComponent<Rigidbody>();

                if (characterController != null) characterController.enabled = false;
                if (rb != null) rb.isKinematic = true;

                // Teleport the player to the destination
                player.transform.position = destination.position;

                // Reactivate CharacterController or Rigidbody
                if (characterController != null) characterController.enabled = true;
                if (rb != null) rb.isKinematic = false;

                Debug.Log("Player teleported to destination: " + destination.position);
            }
        }
        else
        {
            Debug.LogError("Player or destination is not assigned in VentDoor script.");
        }
    }

    // Method to check if vent is open
    public bool IsVentOpen()
    {
        return ventIsOpen;
    }
}
