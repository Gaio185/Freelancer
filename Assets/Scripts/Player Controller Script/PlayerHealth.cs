using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5; // Initial health value
    public List<Image> healthPoints; // List to hold references to heart icons in the UI

    private void Start()
    {
        // Initialize health to match the number of hearts, if they’re not equal
        health = Mathf.Min(health, healthPoints.Count);
        UpdateHealthUI();
    }

    public void TakeDamage()
    {
        // Reduce health by 1
        health -= 1;

        // Update the UI to reflect the new health
        UpdateHealthUI();

        // Check if health is depleted
        if (health <= 0)
        {
            SceneManager.LoadScene(0); // Reload scene on death
        }
    }

    private void UpdateHealthUI()
    {
        // Loop through each heart in the healthPoints list
        for (int i = 0; i < healthPoints.Count; i++)
        {
            // If i is less than current health, the heart should be active (visible)
            if (i < health)
            {
                healthPoints[i].enabled = true;
            }
            else
            {
                // Otherwise, disable the heart (make it invisible)
                healthPoints[i].enabled = false;
            }
        }
    }
}
