using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchweapon : MonoBehaviour
{
    // Weapon models
    public GameObject stunBatonModel;
    public GameObject taserGunModel;

    // Item models
    public GameObject screwdriverModel;
    public GameObject coinModel;

    // Gadget models
    public GameObject overrideKeyCardModel;
    public GameObject flashdriveModel;

    // Keep track of active objects
    private GameObject activeWeapon;
    private GameObject activeItem;
    private GameObject activeGadget;

    void Start()
    {
        LoadSelectedGear(); // Load and show the player's selected gear
    }

    void LoadSelectedGear()
    {
        // Load previously selected gear or defaults
        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton"); // Default weapon

        DeactivateAllModels(); // Ensure all models are turned off

        // Activate the selected weapon
        if (selectedWeapon == "StunBaton")
        {
            activeWeapon = stunBatonModel;
            stunBatonModel.SetActive(true); // Show the stun baton model
        }
        else if (selectedWeapon == "TaserGun")
        {
            activeWeapon = taserGunModel;
            taserGunModel.SetActive(true); // Show the taser gun model
        }

        Debug.Log($"Equipped: Weapon - {selectedWeapon}");
    }

    void Update()
    {
        HandleSwitching(); // Check for input to switch items
    }

    void HandleSwitching()
    {
        // Switch to weapons (press 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon();
        }
        // Switch to items (press 2)
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchItem();
        }
        // Switch to gadgets (press 3)
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchGadget();
        }
    }

    void SwitchWeapon()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");

        // Activate the selected weapon
        if (selectedWeapon == "StunBaton")
        {
            activeWeapon = stunBatonModel;
            stunBatonModel.SetActive(true); // Show the stun baton model
        }
        else if (selectedWeapon == "TaserGun")
        {
            activeWeapon = taserGunModel;
            taserGunModel.SetActive(true); // Show the taser gun model
        }

        Debug.Log($"Equipped: Weapon - {selectedWeapon}");
    }

    void SwitchItem()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedItem = PlayerPrefs.GetString("SelectedItem", "Screwdriver");

        // Activate the selected item
        if (selectedItem == "Screwdriver")
        {
            activeItem = screwdriverModel;
            screwdriverModel.SetActive(true); // Show the screwdriver model
            Debug.Log("Equipped: " + selectedItem);
        }
        else if (selectedItem == "Coin")
        {
            activeItem = coinModel;
            coinModel.SetActive(true); // Show the coin model
            Debug.Log("Equipped: " + selectedItem);
        }
    }

    void SwitchGadget()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedGadget = PlayerPrefs.GetString("SelectedGadget", "OverrideKeyCard");

        // Activate the selected gadget
        if (selectedGadget == "OverrideKeyCard")
        {
            activeGadget = overrideKeyCardModel;
            overrideKeyCardModel.SetActive(true); // Show the override keycard model
            Debug.Log("Equipped: " + selectedGadget);
        }
        else if (selectedGadget == "Flashdrive")
        {
            activeGadget = flashdriveModel;
            flashdriveModel.SetActive(true); // Show the flashdrive model
            Debug.Log("Equipped: " + selectedGadget);
        }
    }

    void DeactivateAllModels()
    {
        // Deactivate all models
        stunBatonModel.SetActive(false);
        taserGunModel.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        overrideKeyCardModel.SetActive(false);
        flashdriveModel.SetActive(false);
    }
}
