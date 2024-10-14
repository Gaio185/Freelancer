using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelector : MonoBehaviour
{
    public static string selectedWeapon;

    public GameObject taserGunText;  // Reference to Taser Gun Text UI
    public GameObject stunBatonText; // Reference to Stun Baton Text UI

    public void SelectWeapon(string weaponName)
    {
        selectedWeapon = weaponName;
        PlayerPrefs.SetString("SelectedWeapon", weaponName);
        PlayerPrefs.Save();

        // Hide both texts initially
        taserGunText.SetActive(false);
        stunBatonText.SetActive(false);

        // Show the selected weapon's text
        if (selectedWeapon == "TaserGun")
        {
            taserGunText.SetActive(true);
        }
        else if (selectedWeapon == "StunBaton")
        {
            stunBatonText.SetActive(true);
        }
    }
}
