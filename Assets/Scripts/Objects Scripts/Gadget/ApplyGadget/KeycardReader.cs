using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeycardReader : MonoBehaviour
{
    public Door controlledDoor;           // Assign the Door in the Unity Editor
    public string requiredKeycardID;       // Specific ID for normal keycard access
    private bool playerInRange = false;    // To check if the player is in range of the keycard reader

    private void Update()
    {
        // Check for mouse click only if player is in range
        if (playerInRange && Input.GetMouseButtonDown(0)) 
        {
            TryUnlockDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is in range of the keycard reader
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the range of the keycard reader
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void TryUnlockDoor()
    {
        // Get the player object in range (you may need to adjust this if the player has a different structure)
        Collider player = Physics.OverlapSphere(transform.position, 2f)
            .FirstOrDefault(col => col.CompareTag("Player"));

        if (player == null) return;

        // Check if the player has a normal keycard
        Keycard normalKeycard = player.GetComponent<Keycard>();
        if (normalKeycard != null && normalKeycard.GetCardID() == requiredKeycardID)
        {
            controlledDoor?.Unlock();
            Debug.Log("Door unlocked with keycard ID: " + requiredKeycardID);
            return;
        }

        // Check if the player has an override keycard with remaining uses
        KeycardOverride overrideKeycard = player.GetComponent<KeycardOverride>();
        if (overrideKeycard != null && overrideKeycard.CanUse())
        {
            overrideKeycard.Use();
            controlledDoor?.Unlock();
            Debug.Log("Door unlocked with override keycard.");
        }
        else
        {
            Debug.Log("Access denied. A valid keycard is required.");
        }
    }
}
