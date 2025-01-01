using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Material healthMaterial;
    [SerializeField] private int maxHealth = 5; // Initial health value
    private int health;
    public string segmentCountProperty = "_SegmentCount";
    public string removedSegmentsProperty = "_RemovedSegments";
    //public List<Image> healthPoints; // List to hold references to heart icons in the UI

    private void Start()
    {
        //// Initialize health to match the number of hearts, if they’re not equal
        //health = Mathf.Min(health, healthPoints.Count);
        //UpdateHealthUI();

        healthMaterial.SetFloat(segmentCountProperty, maxHealth);
        healthMaterial.SetFloat(removedSegmentsProperty, 0f);
        health = maxHealth;
    }

    public void TakeDamage()
    {
        // Reduce health by 1
        health -= 1;
        healthMaterial.SetFloat(removedSegmentsProperty, maxHealth - health);
        //// Update the UI to reflect the new health
        //UpdateHealthUI();

        if (health <= 0)
        {
            Cursor.visible = true; // Show cursor when opening workspace
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            SceneManager.LoadScene(0); // Reload scene on death   
        }
    }

    //private void UpdateHealthUI()
    //{
    //    // Loop through each heart in the healthPoints list
    //    for (int i = 0; i < healthPoints.Count; i++)
    //    {
    //        // If i is less than current health, the heart should be active (visible)
    //        if (i < health)
    //        {
    //            healthPoints[i].enabled = true;
    //        }
    //        else
    //        {
    //            // Otherwise, disable the heart (make it invisible)
    //            healthPoints[i].enabled = false;
    //        }
    //    }
    //}
}
