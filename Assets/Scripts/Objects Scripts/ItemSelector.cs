using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    public static string selectedItem;

    // References to the Text objects for each item
    public GameObject screwdriverText;
    public GameObject coinText;  // Reference for the coin text

    public void SelectItem(string itemName)
    {
        selectedItem = itemName;
        PlayerPrefs.SetString("SelectedItem", itemName);
        PlayerPrefs.Save();

        // Hide all item texts first
        screwdriverText.SetActive(false);
        coinText.SetActive(false);

        // Show the relevant item text based on selection
        if (selectedItem == "Screwdriver")
        {
            screwdriverText.SetActive(true);
        }
        else if (selectedItem == "Coin")
        {
            coinText.SetActive(true);
        }

        // Call a method to ensure weapon text visibility if needed
        //EnsureWeaponTextVisibility();
    }

}
