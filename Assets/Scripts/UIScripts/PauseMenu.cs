using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public GameObject HUD;         // Reference to the HUD
    private GameObject player;     // Reference to the player
    private MonoBehaviour cameraController; // Reference to the camera control script

    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // Find the player by tag
        cameraController = player.GetComponentInChildren<CameraController>(); // Replace with your camera control script's name
        pauseMenuUI.SetActive(false); // Ensure the pause menu starts inactive
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Show the pause menu and disable HUD
        pauseMenuUI.SetActive(true);
        HUD.SetActive(false);

        // Disable player movement
        player.GetComponent<PlayerMovement>().canMove = false;

        // Disable camera control
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause the game
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // Hide the pause menu and enable HUD
        pauseMenuUI.SetActive(false);
        HUD.SetActive(true);

        // Enable player movement
        player.GetComponent<PlayerMovement>().canMove = true;

        // Enable camera control
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Resume the game
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
