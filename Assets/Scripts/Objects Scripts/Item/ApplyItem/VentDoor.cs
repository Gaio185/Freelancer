using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoor : MonoBehaviour
{
    public Transform destination;               // Target destination for the vent
    public float interactionDistance = 2f;      // Distance to interact with the vent
    public GameObject playerRef;                // Reference to the player GameObject

    private void Start()
    {
        // Find the player object tagged "Player" if not already assigned
        if (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        EnterVent();
    }

    public void DestroyVentDoor()
    {
        Debug.Log("You use the screwdriver to destroy the vent door.");
        gameObject.SetActive(false);  // Deactivate the vent door
        EnterVent();
    }

    private void EnterVent()
    {
        if (Input.GetKey(KeyCode.F) && playerRef != null && destination != null)
        {
            float distance = Vector3.Distance(playerRef.transform.position, transform.position);

            // Check if player is close enough to the vent to interact
            if (distance <= interactionDistance)
            {
                // Check if player is already at the destination to prevent unnecessary teleport
                if (Vector3.Distance(playerRef.transform.position, destination.position) < 0.1f)
                {
                    Debug.Log("Player is already at the destination, no teleport needed.");
                    return;
                }

                Debug.Log("Teleporting player to destination.");

                // Temporarily disable CharacterController or Rigidbody if present
                CharacterController characterController = playerRef.GetComponent<CharacterController>();
                Rigidbody rb = playerRef.GetComponent<Rigidbody>();

                if (characterController != null) characterController.enabled = false;
                if (rb != null) rb.isKinematic = true;

                // Teleport the player to the destination
                playerRef.transform.position = destination.position;

                // Reactivate CharacterController or Rigidbody
                if (characterController != null) characterController.enabled = true;
                if (rb != null) rb.isKinematic = false;

                Debug.Log("Player teleported to destination: " + destination.position);
            }
        }
    }
}
