using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Door;

public class EnemyDoors : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            Debug.Log("what the sigma");
            door.StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        door.isOpening = true;

        // Play the correct animation based on the door type
        if (door.doorType == DoorType.Regular)
        {
            door.doorAnimator.Play("DoorOpen");  // Assuming "DoorOpen" is for the regular door
        }
        else if (door.doorType == DoorType.Bathroom)
        {
            door.doorAnimator.Play("BathroomDoor");  // Assuming "BathroomDoor" is for the bathroom door
        }

        yield return new WaitForSeconds(5.0f);  // Time for door to open

        // Assuming "New State" is the idle/closed animation for both types
        door.doorAnimator.Play("New State");

        door.isOpening = false;
    }
}
