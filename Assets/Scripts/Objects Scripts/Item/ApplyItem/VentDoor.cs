using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoor : MonoBehaviour
{
    public Transform destination;
    public float interactionDistance = 2f; // Distance to interact with the vent door
    public LayerMask ventTransform; // LayerMask to specify the vent door layer
    public GameObject playerRef;


    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        EnterVent();
    }

    // Method to destroy the vent door
    public void DestroyVentDoor()
    {
        Debug.Log("You use the screwdriver to destroy the vent door.");
        //Destroy(gameObject); // Destroy the vent door GameObject
        gameObject.SetActive(false);
        EnterVent(); // Allow the player to enter the vent (if needed)
    }

    private void EnterVent()
    { // Check if the player presses the F key
        if (Input.GetKey(KeyCode.F))
        {
            Collider[] hitColliders = Physics.OverlapSphere(playerRef.transform.position, 10f);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    VentDoor ventDoor = hitCollider.GetComponent<VentDoor>();
                    if (ventDoor != null && playerRef != null && destination != null)
                    {
                        Debug.Log("VentDoor detected, teleporting player.");

                        // Temporarily disable CharacterController or Rigidbody if present
                        CharacterController characterController = playerRef.GetComponent<CharacterController>();
                        Rigidbody rb = playerRef.GetComponent<Rigidbody>();

                        if (characterController != null) characterController.enabled = false;
                        if (rb != null) rb.isKinematic = true;

                        // Set player position to destination
                        playerRef.transform.position = destination.position;

                        // Reactivate CharacterController or Rigidbody
                        if (characterController != null) characterController.enabled = true;
                        if (rb != null) rb.isKinematic = false;

                        Debug.Log("Player position updated to destination: " + destination.position);
                    }
                }
            }
        }
    }
}
