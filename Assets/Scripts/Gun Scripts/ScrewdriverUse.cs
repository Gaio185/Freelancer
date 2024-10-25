using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverUse : MonoBehaviour
{
    public float interactionRange = 2f;  // How close the player needs to be to the vent
    public LayerMask ventLayer;          // Layer mask for vent objects
    public PlayerWeapon playerInventory;  // Reference to the inventory script

    void Update()
    {
        // If the player presses F, attempt to unscrew the vent, but only if screwdriver is equipped
        if (Input.GetKeyDown(KeyCode.F) && playerInventory.hasScrewdriverEquipped)
        {
            TryOpenVent();
        }
    }

    void TryOpenVent()
    {
        // Cast a ray from the player forward to detect the vent
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, ventLayer))
        {
            // Check if the object hit by the ray is a vent
            if (hit.collider.CompareTag("Vent"))
            {
                // Call the method to open the vent
                OpenVent(hit.collider.gameObject);
            }
        }
    }

    void OpenVent(GameObject vent)
    {
        // Find the vent door (child object) and make it removable
        GameObject ventDoor = vent.transform.Find("VentDoor").gameObject;
        ventDoor.GetComponent<Rigidbody>().isKinematic = false;  // Enable physics on the door
        ventDoor.GetComponent<Collider>().enabled = true;        // Enable collider

        Debug.Log("Vent door is now removable because screwdriver was used!");
    }
}
