using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    public static string selectedItem;

    // References to the Text objects for each weapon/item
    public GameObject screwdriverText;

    public void SelectItem(string itemName)
    {
        selectedItem = itemName;
        PlayerPrefs.SetString("SelectedItem", itemName);
        PlayerPrefs.Save();

        // Hide the screwdriver text first
        screwdriverText.SetActive(false);

        // Show the relevant item text
        if (selectedItem == "Screwdriver")
        {
            screwdriverText.SetActive(true);
        }

        // Call a method to ensure weapon text visibility
        //EnsureWeaponTextVisibility();
    }

}
