using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelector : MonoBehaviour
{
    public static string selectedWeapon;
    

    // References to the Text objects for each weapon/item
    public GameObject taserGunText;
    public GameObject stunBatonText;
   

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
}
