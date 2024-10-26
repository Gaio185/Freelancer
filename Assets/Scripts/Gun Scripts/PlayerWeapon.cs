using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject stunBatonModel;  // Assign this to the StunBaton model in the WeaponHolder
    public GameObject taserGunModel;   // Assign this to the TaserGun model in the WeaponHolder
    public GameObject screwdriverModel; // Assign this to the Screwdriver model in the ItemHolder

    public bool hasScrewdriverEquipped = false; // Tracks if the screwdriver is equipped
    public bool hasWeaponEquipped = false;     // Tracks if a weapon is equipped
    public string selectedWeapon;   // Holds the selected weapon from PlayerPrefs
    public string selectedItem;     // Holds the selected item from PlayerPrefs

    void Start()
    {
        EquipSelectedGear();  // Equip based on player selection in the menu
    }

    void Update()
    {
        // Check for input to switch between weapons and items
        HandleWeaponSwitching();
    }

    void EquipSelectedGear()
    {
        // Retrieve the weapon and item selection from PlayerPrefs
        selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        selectedItem = PlayerPrefs.GetString("SelectedItem", "None");

        // Deactivate all models first
        stunBatonModel.SetActive(false);
        taserGunModel.SetActive(false);
        screwdriverModel.SetActive(false);

        // Activate the selected weapon
        if (selectedWeapon == "TaserGun")
        {
            taserGunModel.SetActive(true);
            hasWeaponEquipped = true;
        }
        else if (selectedWeapon == "StunBaton")
        {
            stunBatonModel.SetActive(true);
            hasWeaponEquipped = true;
        }

        // Activate the selected item (screwdriver)
        if (selectedItem == "Screwdriver")
        {
            screwdriverModel.SetActive(false);  // Start with the screwdriver hidden until it's selected in-game
            hasScrewdriverEquipped = false;
        }
    }

    void HandleWeaponSwitching()
    {
        // Equip the weapon when pressing '1'
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (selectedWeapon == "TaserGun")
            {
                EquipTaserGun();
            }
            else if (selectedWeapon == "StunBaton")
            {
                EquipStunBaton();
            }

            UnequipScrewdriver();
        }

        // Equip the screwdriver when pressing '2'
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipScrewdriver();
            UnequipWeapon();
        }
    }

    void EquipTaserGun()
    {
        // Equip taser gun, deactivate the stun baton and screwdriver
        taserGunModel.SetActive(true);
        stunBatonModel.SetActive(false);
        screwdriverModel.SetActive(false);
        hasWeaponEquipped = true;
        hasScrewdriverEquipped = false;
        Debug.Log("Taser Gun equipped.");
    }

    void EquipStunBaton()
    {
        // Equip stun baton, deactivate the taser gun and screwdriver
        stunBatonModel.SetActive(true);
        taserGunModel.SetActive(false);
        screwdriverModel.SetActive(false);
        hasWeaponEquipped = true;
        hasScrewdriverEquipped = false;
        Debug.Log("Stun Baton equipped.");
    }

    void EquipScrewdriver()
    {
        // Equip screwdriver, deactivate both the taser gun and stun baton
        screwdriverModel.SetActive(true);
        stunBatonModel.SetActive(false);
        taserGunModel.SetActive(false);
        hasScrewdriverEquipped = true;
        hasWeaponEquipped = false;
        Debug.Log("Screwdriver equipped.");
    }

    void UnequipWeapon()
    {
        // Deactivate both weapons
        taserGunModel.SetActive(false);
        stunBatonModel.SetActive(false);
        hasWeaponEquipped = false;
        Debug.Log("Weapon unequipped.");
    }

    void UnequipScrewdriver()
    {
        // Deactivate the screwdriver
        screwdriverModel.SetActive(false);
        hasScrewdriverEquipped = false;
        Debug.Log("Screwdriver unequipped.");
    }

    // Call this method from WeaponSelector to update selections
    public void UpdateSelection(string weapon, string item)
    {
        selectedWeapon = weapon;
        selectedItem = item;
        EquipSelectedGear();  // Re-equip the gear based on the new selections
    }
}
