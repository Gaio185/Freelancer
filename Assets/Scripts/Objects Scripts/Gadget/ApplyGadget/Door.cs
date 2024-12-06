using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public bool isUnlocked = false;  // Track if the door is unlocked
    public bool isOpening = false;  // Is the door currently opening or closing?

    public Animator doorAnimator;  // Animator for the door

    // Enum to define door types
    public enum DoorType
    {
        Regular,
        Bathroom
    }

    // The type of the door
    public DoorType doorType = DoorType.Regular;

    private bool isOpen = false;  // Track if the door is open or closed

    // Method to unlock the door
    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Door is now unlocked.");
    }

    // Method to try opening or closing the door
    public void TryToggleDoor()
    {
        if (isUnlocked && !isOpening)
        {
            StartCoroutine(ToggleDoor());
        }
        else if (!isUnlocked)
        {
            Debug.Log("The door is locked. Use a keycard to unlock it first.");
        }
    }

    // Coroutine to toggle the door
    IEnumerator ToggleDoor()
    {
        isOpening = true;

        // If the door is closed, open it
        if (!isOpen)
        {
            // Open the door
            if (doorType == DoorType.Regular)
            {
                doorAnimator.Play("DoorOpen", -1, 0f); // Start opening from 0s to 1s
                doorAnimator.speed = 1; // Ensure animation plays at normal speed
            }
            else if (doorType == DoorType.Bathroom)
            {
                doorAnimator.Play("BathroomDoor", -1, 0f); // Bathroom door opening part
                doorAnimator.speed = 1; // Ensure animation plays at normal speed
            }

            // Wait for the opening animation to finish (1s mark)
            yield return new WaitForSeconds(1f);

            // After opening, freeze the animation at the open state (just keep the door open)
            doorAnimator.speed = 0;  // Stop the animation after it opens

            isOpen = true;  // Mark the door as open
        }
        else
        {
            // If the door is open, close it
            if (doorType == DoorType.Regular)
            {
                doorAnimator.Play("DoorOpen", -1, 0.8f); // Start closing from 4s to 5s
                doorAnimator.speed = 1; // Ensure animation plays at normal speed
            }
            else if (doorType == DoorType.Bathroom)
            {
                doorAnimator.Play("BathroomDoor", -1, 0.8f); // Bathroom door closing part
                doorAnimator.speed = 1; // Ensure animation plays at normal speed
            }

            // Wait for the closing animation to finish (until 5s)
            yield return new WaitForSeconds(1f);

            // After closing, freeze the animation at the closed state
            doorAnimator.speed = 0;  // Stop the animation after it closes

            isOpen = false;  // Mark the door as closed
        }

        isOpening = false;  // Allow the player to interact again
    }
}
