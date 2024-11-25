using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause menu UI

    private Player player; // Player object
    private MonoBehaviour cameraController; // Camera controller (replace with your script)

    // Serialized fields to store button animators for the pause menu
    [SerializeField] private Animator[] pauseMenuButtonAnimators; // Button animators in the Pause Menu

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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

        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause Menu UI is not assigned in the Inspector.");
        }

        if (player.HUD == null)
        {
            Debug.LogError("HUD UI is not assigned in the Inspector.");
        }

        pauseMenuUI.SetActive(false); // Ensure pause menu starts hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else if(player.canPause)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenuUI == null || player.HUD == null || player == null) return;

        // Show pause menu and hide HUD
        pauseMenuUI.SetActive(true);
        player.HUD.SetActive(false);

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

        // Pause game time but allow animations
        Time.timeScale = 0f;

        // Play button animations for the pause menu
        PlayPauseButtonAnimations();
    }

    public void ResumeGame()
    {
        if (pauseMenuUI == null || player.HUD == null || player == null) return;

        // Hide pause menu and show HUD
        pauseMenuUI.SetActive(false);
        player.HUD.SetActive(true);

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

    // Function to play animations for the buttons in the pause menu
    private void PlayPauseButtonAnimations()
    {
        if (pauseMenuButtonAnimators == null || pauseMenuButtonAnimators.Length == 0)
        {
            Debug.LogError("No Pause Menu button animators assigned!");
            return;
        }

        foreach (var animator in pauseMenuButtonAnimators)
        {
            if (animator != null)
            {
                // Ensure the animator uses unscaled time
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;

                // Play the 'Idle' animation immediately for all buttons
                animator.Play("Idle", 0, 0f); // Play the 'Idle' animation from the beginning
            }
        }
    }

    // Called when a button is clicked
    public void OnButtonClick(Animator buttonAnimator)
    {
        if (buttonAnimator != null)
        {
            buttonAnimator.Play("Pressed", 0, 0f); // Play the 'Pressed' animation when clicked
        }
    }

    // Method to handle button hover
    public void OnButtonHover(Animator buttonAnimator)
    {
        if (buttonAnimator != null)
        {
            buttonAnimator.Play("Highlight", 0, 0f); // Play the 'Highlight' animation on hover
        }
    }

    // Method to handle button exit hover
    public void OnButtonExit(Animator buttonAnimator)
    {
        if (buttonAnimator != null)
        {
            buttonAnimator.Play("Idle", 0, 0f); // Revert to 'Idle' animation when hover ends
        }
    }

    // Scene Management
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
