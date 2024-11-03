using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardReader : MonoBehaviour
{
    public Door connectedDoor;         // The door controlled by this reader
    public string requiredKeycardID;    // The specific ID for normal keycard access

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has a normal keycard
        Keycard normalKeycard = other.GetComponent<Keycard>();

        if (normalKeycard != null && normalKeycard.GetCardID() == requiredKeycardID)
        {
            connectedDoor.Unlock();
           Debug.Log("Door unlocked with keycard ID: " + requiredKeycardID);
           return;
        }

        // Check if the player has an override keycard with remaining uses
        KeycardOverride overrideKeycard = other.GetComponent<KeycardOverride>();

        if (overrideKeycard != null && overrideKeycard.CanUse())
        {
            overrideKeycard.Use();
            connectedDoor.Unlock();
            Debug.Log("Door unlocked with override keycard.");
        }
        else
        {
            Debug.Log("Access denied. A valid keycard is required.");
        }
    }
}
