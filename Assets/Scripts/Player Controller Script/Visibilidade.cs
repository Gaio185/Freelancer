using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibilidade : MonoBehaviour
{
    // This variable determines if the cube should have any specific behavior when the player is inside.
    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            // Optional: Logic when the player enters the cube
            playerInside = true;
            Debug.Log("Player entered the cube.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger
        if (other.CompareTag("Player"))
        {
            // Optional: Logic when the player exits the cube
            playerInside = false;
            Debug.Log("Player exited the cube.");
        }
    }
}
