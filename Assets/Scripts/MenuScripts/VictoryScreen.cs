using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreenUI; // Reference to the UI (Canvas with Text/Buttons)
    public float displayTime = 3f;     // Time to show victory screen before switching scenes

    void Start()
    {
        victoryScreenUI.SetActive(true); // Show the victory screen UI

        // Automatically return to the menu after a delay, unless the button is clicked
        Invoke("Menu", displayTime);
    }

    public void ReturnToMenu()
    {
        // Load the main menu or another scene after victory
        SceneManager.LoadScene("Menu"); // Replace "MenuScene" with the actual name of your menu scene
    }
}
