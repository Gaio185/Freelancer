using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static Door;

public class EnemyDoors : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Worker"))
        {
            StartCoroutine(ToggleDoor());
        }
    }
    
    IEnumerator ToggleDoor()
    {
        door.isOpening = true;

        // If the door is closed, open it
        if (!door.isOpen)
        {
            // Open the door
            if (door.doorType == DoorType.Regular)
            {
                door.doorAnimator.Play("DoorOpen", -1, 0f); // Start opening from 0s to 1s
                door.doorAnimator.speed = 1; // Ensure animation plays at normal speed
            }
            else if (door.doorType == DoorType.Bathroom)
            {
                door.doorAnimator.Play("BathroomDoor", -1, 0f); // Bathroom door opening part
                door.doorAnimator.speed = 1; // Ensure animation plays at normal speed
            }

            // Wait for the opening animation to finish (1s mark)
            yield return new WaitForSeconds(1f);
        }

        door.isOpening = false;  // Allow the player to interact again
    }
}
