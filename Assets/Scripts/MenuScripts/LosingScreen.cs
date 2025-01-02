using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingScreen : MonoBehaviour
{
    public GameObject losingScreenUI; // Reference to the UI (Canvas with Text/Buttons)
    public float displayTime = 3f;     // Time to show losing screen before switching scenes

    void Start()
    {
        losingScreenUI.SetActive(true); // Show the losing screen UI

        // Automatically return to the menu after a delay (optional)
        Invoke("Menu", displayTime);
    }

    public void ReturnToMenu()
    {
        // Load the main menu or another scene
        SceneManager.LoadScene("Menu"); // Replace with your menu scene name
    }

    public void RestartGame()
    {
        // Reload the game scene
        SceneManager.LoadScene("Alpha"); // Replace "Alpha" with your game scene name
    }
}
