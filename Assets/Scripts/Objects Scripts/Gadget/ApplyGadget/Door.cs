using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 openPositionOffset = new Vector3(0, 0, 3); // How far the door moves when opening
    private Vector3 closedPosition;
    private bool isLocked = true;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("Door unlocked!");
    }

    public void ToggleDoor()
    {
        if (!isLocked)
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        else
        {
            Debug.Log("Door is locked. Please use a keycard.");
        }
    }

    void OpenDoor()
    {
        transform.position = closedPosition + openPositionOffset;
        isOpen = true;
        Debug.Log("Door opened.");
    }

    void CloseDoor()
    {
        transform.position = closedPosition;
        isOpen = false;
        Debug.Log("Door closed.");
    }
}
