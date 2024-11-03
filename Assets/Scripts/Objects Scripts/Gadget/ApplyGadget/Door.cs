using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    public float open = 100f;
    public float range = 10f;

    public GameObject door;
    public bool isOpening = false;
    private bool isUnlocked = false;     // Tracks if the door is unlocked

    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Shoot();
        }
    }

    // Method to unlock the door, can be called by KeycardReader or other scripts
    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Door is now unlocked.");
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            DoorBehaviour target = hit.transform.GetComponent<DoorBehaviour>();
            Debug.Log(target);
            if (target != null && isUnlocked)    // Check if the door is unlocked before opening
            {
                StartCoroutine(OpenDoor());
            }
            else if (!isUnlocked)
            {
                Debug.Log("The door is locked. Use a keycard to unlock it first.");
            }
        }
    }

    IEnumerator OpenDoor()
    {
        isOpening = true;
        door.GetComponent<Animator>().Play("DoorOpen");
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(5.0f);
        door.GetComponent<Animator>().Play("New State");
        isOpening = false;
    }
}
