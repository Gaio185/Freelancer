using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // References to the main buttons
    public Button smuggledButton;
    public Button regularButton;
    public Button gadgetsButton;

    // References to sub-option panels (make sure these are child objects of their main buttons)
    public GameObject smuggledSubOptions;
    public GameObject regularSubOptions;
    public GameObject gadgetsSubOptions;

    // Tracks the currently active main button and panel
    private Button currentActiveButton = null;
    private GameObject currentActivePanel = null;

    // When a main button is clicked
    public void OnMainButtonClick(Button clickedButton)
    {
        // Check if the clicked button is already the active one
        if (clickedButton == currentActiveButton)
        {
            // Close the currently active panel
            currentActivePanel.SetActive(false);

            // Reset button animations
            ResetButtonState(clickedButton);

            currentActiveButton = null;
            currentActivePanel = null;

            // Re-enable all main buttons
            SetMainButtonsInteractable(true);
        }
        else
        {
            // Close any currently active panel before opening a new one
            if (currentActivePanel != null)
            {
                currentActivePanel.SetActive(false);
                ResetButtonState(currentActiveButton);
            }

            // Disable all other main buttons
            SetMainButtonsInteractable(false);
            clickedButton.interactable = true; // Keep the clicked button active

            // Show the corresponding sub-options and track them
            if (clickedButton == smuggledButton)
            {
                smuggledSubOptions.SetActive(true);
                currentActivePanel = smuggledSubOptions;
            }
            else if (clickedButton == regularButton)
            {
                regularSubOptions.SetActive(true);
                currentActivePanel = regularSubOptions;
            }
            else if (clickedButton == gadgetsButton)
            {
                gadgetsSubOptions.SetActive(true);
                currentActivePanel = gadgetsSubOptions;
            }

            currentActiveButton = clickedButton; // Track the active button
        }
    }

    // When a sub-option is clicked
    public void OnSubOptionClick()
    {
        // Re-enable all main buttons
        SetMainButtonsInteractable(true);

        // Hide the current active panel
        if (currentActivePanel != null)
        {
            currentActivePanel.SetActive(false);
            ResetButtonState(currentActiveButton);
        }

        // Reset the tracking variables
        currentActiveButton = null;
        currentActivePanel = null;
    }

    // Helper function to enable/disable all main buttons
    private void SetMainButtonsInteractable(bool state)
    {
        smuggledButton.interactable = state;
        regularButton.interactable = state;
        gadgetsButton.interactable = state;
    }

    // Helper function to reset button animations
    private void ResetButtonState(Button button)
    {
        // Temporarily disable and re-enable the button to trigger state change
        button.interactable = false;
        button.interactable = true;
    }
}
