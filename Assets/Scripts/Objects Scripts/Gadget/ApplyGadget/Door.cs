using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public bool isUnlocked = false;  // Track if the door is unlocked
    public GameObject doorObject;    // The actual door GameObject to animate
    public bool isOpening = false;  // Is the door currently opening?

    public Animator doorAnimator;  // To play animations

    // Enum to define door types
    public enum DoorType
    {
        Regular,
        Bathroom
    }

    // The type of the door
    public DoorType doorType = DoorType.Regular;

    // Method to unlock the door
    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Door is now unlocked.");
    }

    // Opens the door (only if unlocked)
    public void TryOpenDoor()
    {
        if (isUnlocked && !isOpening)
        {
            StartCoroutine(OpenDoor());
        }
        else if (!isUnlocked)
        {
            Debug.Log("The door is locked. Use a keycard to unlock it first.");
        }
    }

    // Coroutine to open the door with animation
    IEnumerator OpenDoor()
    {
        isOpening = true;

        // Play the correct animation based on the door type
        if (doorType == DoorType.Regular)
        {
            doorAnimator.Play("DoorOpen");  // Assuming "DoorOpen" is for the regular door
        }
        else if (doorType == DoorType.Bathroom)
        {
            doorAnimator.Play("BathroomDoor");  // Assuming "BathroomDoor" is for the bathroom door
        }

        yield return new WaitForSeconds(5.0f);  // Time for door to open

        // Assuming "New State" is the idle/closed animation for both types
        doorAnimator.Play("New State");

        isOpening = false;
    }
}
