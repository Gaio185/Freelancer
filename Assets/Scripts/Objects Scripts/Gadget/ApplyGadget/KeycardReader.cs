using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeycardReader : MonoBehaviour
{
    public Door controlledDoor;  // The door this reader unlocks
    public DivisionType requiredDivisionType;  // The required division type for this reader
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode.F)) && !controlledDoor.isUnlocked)  // Use mouse click for interaction
        {
            TryUnlock();
        }
    }

    private void TryUnlock()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 3f);  // Detect nearby colliders

        foreach (Collider collider in nearbyColliders)
        {
            // Check for KeycardOverride (this will unlock the door regardless of the division type)
            KeycardOverride overrideKeycard = collider.GetComponent<KeycardOverride>();
            if (overrideKeycard != null && overrideKeycard.CanUse())
            {
                overrideKeycard.Use();
                controlledDoor?.Unlock();  // Unlock the controlled door
                Debug.Log("Door unlocked with override keycard.");
                return;
            }

            // Check for a regular Keycard
            for(int i = 0; i < player.GetComponent<Player>().keycards.Count; i++)
            {
                Keycard keycard = player.GetComponent<Player>().keycards[i];
                if (keycard.GetDivisionType() == requiredDivisionType)
                {
                    controlledDoor?.Unlock();  // Unlock the controlled door
                    Debug.Log("Door unlocked with keycard for " + requiredDivisionType);
                    return;
                }
            }
        }

        Debug.Log("Access denied. A valid keycard or override keycard is required.");
    }
}
