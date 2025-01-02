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

    private void Start()
    {
        healthMaterial.SetFloat(segmentCountProperty, maxHealth);
        healthMaterial.SetFloat(removedSegmentsProperty, 0f);
        health = maxHealth;
    }

    public void TakeDamage()
    {
        // Reduce health by 1
        health -= 1;
        healthMaterial.SetFloat(removedSegmentsProperty, maxHealth - health);

        if (health <= 0)
        {
            ShowLosingScreen(); // Call the Losing Screen transition
        }
    }

    private void ShowLosingScreen()
    {
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI navigation
        SceneManager.LoadScene("LosingScreen"); // Transition to the Losing Scene
    }
}
