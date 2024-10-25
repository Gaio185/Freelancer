using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelector : MonoBehaviour
{
    public static string selectedWeapon;
    //public static string selectedItem;

    // References to the Text objects for each weapon/item
    public GameObject taserGunText;
    public GameObject stunBatonText;
    //public GameObject screwdriverText;

    public void SelectWeapon(string weaponName)
    {
        selectedWeapon = weaponName;
        PlayerPrefs.SetString("SelectedWeapon", weaponName);
        PlayerPrefs.Save();

        // Hide all weapon texts first
        taserGunText.SetActive(false);
        stunBatonText.SetActive(false);

        // Show the relevant weapon text
        if (selectedWeapon == "TaserGun")
        {
            taserGunText.SetActive(true);
        }
        else if (selectedWeapon == "StunBaton")
        {
            stunBatonText.SetActive(true);
        }

        // Update the PlayerPrefs (if not already done)
        PlayerPrefs.SetString("SelectedWeapon", selectedWeapon);
        PlayerPrefs.Save();
    }

    //public void SelectItem(string itemName)
   // {
       // selectedItem = itemName;
       // PlayerPrefs.SetString("SelectedItem", itemName);
       // PlayerPrefs.Save();

        // Hide the screwdriver text first
       // screwdriverText.SetActive(false);

        // Show the relevant item text
        //if (selectedItem == "Screwdriver")
        //{
           // screwdriverText.SetActive(true);
       // }

        // Call a method to ensure weapon text visibility
        //EnsureWeaponTextVisibility();
    //}

    //private void EnsureWeaponTextVisibility()
   // {
        // Show the relevant weapon text based on what's currently selected
        //if (PlayerPrefs.GetString("SelectedWeapon", "StunBaton") == "TaserGun")
       // {
        //    taserGunText.SetActive(true);
       // }
     //   else if (PlayerPrefs.GetString("SelectedWeapon", "StunBaton") == "StunBaton")
        //{
           // stunBatonText.SetActive(true);
      //  }

    //}
}
