using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify ObjectiveManager to update the objective
            ObjectiveManager manager = FindObjectOfType<ObjectiveManager>();
            if (manager != null)
            {
                manager.CompleteObjective();
            }

            // Destroy the trigger to prevent repeated use
            Destroy(gameObject);
        }
    }
}
