using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoor : MonoBehaviour
{
    // Method to destroy the vent door
    public void DestroyVentDoor()
    {
        Debug.Log("You use the screwdriver to destroy the vent door.");
        Destroy(gameObject); // Destroy the vent door GameObject
        EnterVent(); // Allow the player to enter the vent (if needed)
    }

    private void EnterVent()
    {
        // Logic for entering the vent (optional)
        Debug.Log("You crawl into the vent.");
    }
}
