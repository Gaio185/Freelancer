using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public bool isUnlocked = false;  // Track if the door is unlocked
    public GameObject doorObject;    // The actual door GameObject to animate
    private bool isOpening = false;  // Is the door currently opening?

    public Animator doorAnimator;  // To play animations

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
        doorAnimator.Play("DoorOpen");  // Assuming you have an "Open" animation
        yield return new WaitForSeconds(5.0f);  // Time for door to open
        doorAnimator.Play("New State");  // Assuming "New State" is the idle/closed animation
        isOpening = false;
    }
}
