using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject optionsMenuUI;
    public GameObject pauseMenuUI;
    public GameObject hudUI;

    [Header("Buttons Animators")]
    public Animator[] optionsMenuButtonAnimators;

    [Header("Navigation Settings")]
    public GameObject optionsFirstButton; // First button to highlight when opening the menu

    private void Start()
    {
        // Ensure Animators are using Unscaled Time for paused menus
        foreach (Animator animator in optionsMenuButtonAnimators)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    public void OpenOptionsMenu()
    {
        // Switch to Options Menu
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        hudUI.SetActive(false);

        // Ensure EventSystem focuses on the first button in the menu
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptionsMenu()
    {
        // Return to Pause Menu
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        hudUI.SetActive(false);

        // Reset EventSystem focus to Pause Menu's first button
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnButtonHover(Animator buttonAnimator)
    {
        // Play Highlight animation
        buttonAnimator.Play("Highlight", 0, 0f);
    }

    public void OnButtonExit(Animator buttonAnimator)
    {
        // Play Normal animation
        buttonAnimator.Play("Normal", 0, 0f);
    }
}
