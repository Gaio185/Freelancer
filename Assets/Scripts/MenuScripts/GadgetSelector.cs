using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GadgetSelector : MonoBehaviour
{
    public static string selectedGadget;

    // References to the Text objects for each item
    public GameObject keycardText;
    public GameObject flashDriveText;  // Reference for the coin text

    public void SelectItem(string gadgetName)
    {
        selectedGadget = gadgetName;
        PlayerPrefs.SetString("SelectedGadget", gadgetName);
        PlayerPrefs.Save();

        // Hide all item texts first
        keycardText.SetActive(false);
        flashDriveText.SetActive(false);

        // Show the relevant item text based on selection
        if (selectedGadget == "KeycardOverride")
        {
            keycardText.SetActive(true);
        }
        else if (selectedGadget == "USBPenOverride")
        {
            flashDriveText.SetActive(true);
        }

        // Call a method to ensure weapon text visibility if needed
        //EnsureWeaponTextVisibility();
    }
}
