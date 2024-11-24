using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause menu UI
    public GameObject optionsMenuUI; // Options menu UI
    public GameObject HUD; // HUD UI

    private GameObject player; // Player object
    private MonoBehaviour cameraController; // Camera controller (replace with your script)

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Ensure the Player has the 'Player' tag.");
            return;
        }

        cameraController = player.GetComponentInChildren<CameraController>();
        if (cameraController == null)
        {
            Debug.LogWarning("CameraController not found on Player or its children.");
        }

        if (pauseMenuUI == null || optionsMenuUI == null)
        {
            Debug.LogError("Pause Menu or Options Menu UI is not assigned in the Inspector.");
        }

        if (HUD == null)
        {
            Debug.LogError("HUD UI is not assigned in the Inspector.");
        }

        pauseMenuUI.SetActive(false); // Ensure pause menu starts hidden
        optionsMenuUI.SetActive(false); // Ensure options menu starts hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else if (!optionsMenuUI.activeSelf) // Prevent toggling while in Options
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenuUI == null || HUD == null || player == null) return;

        // Show pause menu and hide HUD
        pauseMenuUI.SetActive(true);
        HUD.SetActive(false);

        // Disable player movement
        var playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.canMove = false;
        }

        // Disable camera control
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause game time but allow unscaled UI animations
        Time.timeScale = 0f;

        // Play button animations
        PlayButtonAnimations(pauseMenuUI);
    }

    public void ResumeGame()
    {
        if (pauseMenuUI == null || HUD == null || player == null) return;

        // Hide pause menu and show HUD
        pauseMenuUI.SetActive(false);
        HUD.SetActive(true);

        // Enable player movement
        var playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.canMove = true;
        }

        // Enable camera control
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Resume game time
        Time.timeScale = 1f;
    }

    public void OpenOptionsMenu()
    {
        if (optionsMenuUI == null || pauseMenuUI == null) return;

        // Hide pause menu and show options menu
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);

        // Play button animations for the options menu
        PlayButtonAnimations(optionsMenuUI);
    }

    public void CloseOptionsMenu()
    {
        if (optionsMenuUI == null || pauseMenuUI == null) return;

        // Hide options menu and show pause menu
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        // Play button animations for the pause menu
        PlayButtonAnimations(pauseMenuUI);
    }

    private void PlayButtonAnimations(GameObject menuUI)
    {
        Animator[] buttonAnimators = menuUI.GetComponentsInChildren<Animator>();
        foreach (var animator in buttonAnimators)
        {
            if (animator != null)
            {
                // Ensure the animator uses unscaled time
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;

                // Optionally, trigger a specific animation (e.g., "Idle")
                animator.Play("Idle", -1, 0f);
            }
        }
    }

    public void OnButtonClick()
    {
        Debug.Log("Button clicked!");
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
