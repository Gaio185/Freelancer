using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GadgetSelector : MonoBehaviour
{
    // Public GameObjects for the buttons
    public GameObject button1; // Assign the first button in the Inspector
    public GameObject button2; // Assign the second button in the Inspector

    private string[] gadgets = { "OverrideKeyCard", "Flashdrive" }; // Names for the two gadgets
    private GameObject activeButton; // To keep track of the currently active button

    void Start()
    {
        SetupButtons();
    }

    void SetupButtons()
    {
        // Setup button 1
        Button btn1 = button1.GetComponent<Button>();
        btn1.GetComponentInChildren<Text>().text = gadgets[0]; // Set the text for button 1
        btn1.onClick.AddListener(() => SelectGadget(gadgets[0], button1)); // Set up the click listener for button 1

        // Setup button 2
        Button btn2 = button2.GetComponent<Button>();
        btn2.GetComponentInChildren<Text>().text = gadgets[1]; // Set the text for button 2
        btn2.onClick.AddListener(() => SelectGadget(gadgets[1], button2)); // Set up the click listener for button 2
    }

    public void SelectGadget(string gadgetName, GameObject selectedButton)
    {
        // Deactivate the previously active button if there is one
        if (activeButton != null && activeButton != selectedButton)
        {
            activeButton.SetActive(false); // Hide the previously selected button
        }

        activeButton = selectedButton; // Update the active button
        activeButton.SetActive(true); // Show the currently selected button

        Debug.Log($"{gadgetName} selected"); // Handle the selection
        // Additional logic to handle the selected gadget goes here
    }
}
