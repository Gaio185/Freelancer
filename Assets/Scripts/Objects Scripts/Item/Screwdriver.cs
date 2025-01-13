using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    public float interactionDistance = 5f; // Distance for screwdriver interaction with a vent
    private GameObject closestVent; // The closest vent within interaction range
    private VentDoor ventScript; // The VentDoor script attached to the closest vent

    public GameObject screwdriverUI; // Reference to the screwdriver UI object
    public LayerMask ventLayer; // Layer mask to detect vents (can be set in Inspector)

    public GameObject player; // Manually reference the player here

    private void OnEnable()
    {
        screwdriverUI.SetActive(true); // Make the screwdriver UI active when enabled
    }

    private void OnDisable()
    {
        screwdriverUI.SetActive(false); // Make the screwdriver UI inactive when disabled
    }

    //void Update()
    //{
    //    // Find all colliders in range, filtering only by ventLayer
    //    Collider[] ventsInRange = Physics.OverlapSphere(transform.position, interactionDistance, ventLayer);
    //    closestVent = null;

    //    // Check which vent is the closest
    //    float closestDistance = Mathf.Infinity;
    //    foreach (var collider in ventsInRange)
    //    {
    //        float distance = Vector3.Distance(transform.position, collider.transform.position);
    //        if (distance < closestDistance)
    //        {
    //            closestVent = collider.gameObject;
    //            closestDistance = distance;
    //        }
    //    }

    //    // If there is a vent within range and it's the closest, handle interaction
    //    if (closestVent != null)
    //    {
    //        ventScript = closestVent.GetComponent<VentDoor>();
    //        if (ventScript != null && Input.GetMouseButtonDown(0)) // Left-click to open the vent
    //        {
    //            ventScript.OpenVent(); // This will deactivate the vent's MeshRenderer
    //            Debug.Log("Screwdriver has opened the vent.");
    //        }
    //    }

    //    // Handle teleportation if the vent is open
    //    if (ventScript != null && ventScript.IsVentOpen() && Input.GetKey(KeyCode.F))
    //    {
    //        if (player != null)
    //        {
    //            ventScript.EnterVent(); // Teleport the player to the destination
    //        }
    //        else
    //        {
    //            Debug.LogError("Player reference is missing! Please assign the player in the Inspector.");
    //        }
    //    }
    //}
}
