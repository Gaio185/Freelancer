using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public List<Button> categoryButtons; // Assign all category buttons in the Inspector
    private Button activeCategoryButton = null; // Tracks the currently active button

    public void OnCategoryButtonClicked(Button clickedButton)
    {
        // If another button is active, prevent this button from being clicked
        if (activeCategoryButton != null && activeCategoryButton != clickedButton)
        {
            Debug.Log("Finish the current selection before proceeding.");
            return;
        }

        // Set this button as the active button and disable all other buttons
        activeCategoryButton = clickedButton;
        foreach (Button button in categoryButtons)
        {
            if (button != clickedButton)
            {
                button.interactable = false; // Disable other buttons
            }
        }

        Debug.Log($"{clickedButton.name} category selected. Make your selection now.");
    }

    public void OnSelectionConfirmed()
    {
        // Reactivate all buttons
        foreach (Button button in categoryButtons)
        {
            button.interactable = true;
        }

        Debug.Log($"{activeCategoryButton.name} selection confirmed.");
        activeCategoryButton = null; // Reset the active category
    }
}
