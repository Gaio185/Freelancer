using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject stunBatonModel;     // Assign this to the StunBaton model in the WeaponHolder
    public GameObject taserGunModel;      // Assign this to the TaserGun model in the WeaponHolder
    public GameObject screwdriverModel;   // Assign this to the Screwdriver model in the ItemHolder
    public GameObject coinModel;          // Assign this to the Coin model in the ItemHolder

    public bool hasItemEquipped = false;  // Tracks if an item is equipped (screwdriver or coin)
    public bool hasWeaponEquipped = false; // Tracks if a weapon is equipped
    public string selectedWeapon;          // Holds the selected weapon from PlayerPrefs
    public string selectedItem;            // Holds the selected item from PlayerPrefs

    void Start()
    {
        // Equip based on player selection in the menu
        EquipSelectedGear();
    }

    void Update()
    {
        // Check for input to switch between weapons and items
        HandleWeaponSwitching();
    }

    void EquipSelectedGear()
    {
        // Retrieve or set default values in PlayerPrefs if not yet saved
        selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        selectedItem = PlayerPrefs.GetString("SelectedItem", "Screwdriver");

        // Deactivate all models first to ensure there is no overlap
        DeactivateAllModels();

        // Activate the selected weapon model only
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

        // Log the currently equipped items for debugging purposes
        Debug.Log("Equipped Weapon: " + selectedWeapon);
        Debug.Log("Equipped Item: " + selectedItem);
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
            UnequipItem();  // Unequip any item when a weapon is equipped
        }

        // Equip the item when pressing '2'
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (selectedItem == "Screwdriver")
            {
                EquipScrewdriver();
            }
            else if (selectedItem == "Coin")
            {
                EquipCoin();
            }
            UnequipWeapon();  // Unequip any weapon when an item is equipped
        }
    }

    void EquipTaserGun()
    {
        taserGunModel.SetActive(true);
        stunBatonModel.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        hasWeaponEquipped = true;
        hasItemEquipped = false;
        Debug.Log("Taser Gun equipped.");
    }

    void EquipStunBaton()
    {
        stunBatonModel.SetActive(true);
        taserGunModel.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        hasWeaponEquipped = true;
        hasItemEquipped = false;
        Debug.Log("Stun Baton equipped.");
    }

    void EquipScrewdriver()
    {
        screwdriverModel.SetActive(true);
        taserGunModel.SetActive(false);
        stunBatonModel.SetActive(false);
        coinModel.SetActive(false);
        hasItemEquipped = true;
        hasWeaponEquipped = false;
        Debug.Log("Screwdriver equipped.");
    }

    void EquipCoin()
    {
        coinModel.SetActive(true);
        taserGunModel.SetActive(false);
        stunBatonModel.SetActive(false);
        screwdriverModel.SetActive(false);
        hasItemEquipped = true;
        hasWeaponEquipped = false;
        Debug.Log("Coin equipped.");
    }

    void UnequipWeapon()
    {
        taserGunModel.SetActive(false);
        stunBatonModel.SetActive(false);
        hasWeaponEquipped = false;
        Debug.Log("Weapon unequipped.");
    }

    void UnequipItem()
    {
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        hasItemEquipped = false;
        Debug.Log("Item unequipped.");
    }

    void DeactivateAllModels()
    {
        // Deactivate all models to prevent overlap
        stunBatonModel.SetActive(false);
        taserGunModel.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
    }

    // Call this method from WeaponSelector to update selections
    public void UpdateSelection(string weapon, string item)
    {
        selectedWeapon = weapon;
        selectedItem = item;
        EquipSelectedGear();  // Re-equip the gear based on the new selections
    }
}

