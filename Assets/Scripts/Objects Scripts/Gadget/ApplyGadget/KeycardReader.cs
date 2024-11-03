using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardReader : MonoBehaviour
{
    public Door controlledDoor;           // Assign the Door in the Unity Editor
    public string requiredKeycardID;       // Specific ID for normal keycard access

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has a normal keycard
        Keycard normalKeycard = other.GetComponent<Keycard>();

        if (normalKeycard != null && normalKeycard.GetCardID() == requiredKeycardID)
        {
            if (controlledDoor != null)
            {
                controlledDoor.Unlock();    // Unlock the door if a valid keycard is detected
                Debug.Log("Door unlocked with keycard ID: " + requiredKeycardID);
            }
            return;
        }

        // Check if the player has an override keycard with remaining uses
        KeycardOverride overrideKeycard = other.GetComponent<KeycardOverride>();

        if (overrideKeycard != null && overrideKeycard.CanUse())
        {
            overrideKeycard.Use();
            if (controlledDoor != null)
            {
                controlledDoor.Unlock();    // Unlock the door if an override keycard is used
                Debug.Log("Door unlocked with override keycard.");
            }
        }
        else
        {
            Debug.Log("Access denied. A valid keycard is required.");
        }
    }
}
