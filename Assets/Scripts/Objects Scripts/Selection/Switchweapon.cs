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

    public bool hasWeaponEquipped;

    // Keep track of active objects
    private GameObject activeWeapon;
    private GameObject activeItem;
    private GameObject activeGadget;

    // Crosshair reference
    public GameObject crosshair;

    //void Start()
    //{
    //    LoadSelectedGear(); // Load and show the player's selected gear
    //}

    //void LoadSelectedGear()
    //{
    //    // Load previously selected gear or defaults
    //    string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton"); // Default weapon

    //    DeactivateAllModels(); // Ensure all models are turned off

    //    // Activate the selected weapon
    //    if (selectedWeapon == "StunBaton")
    //    {
    //        activeWeapon = stunBatonModel;
    //        stunBatonModel.SetActive(true); // Show the stun baton model
    //        crosshair.SetActive(true); // Show crosshair when weapon is equipped
    //    }
    //    else if (selectedWeapon == "TaserGun")
    //    {
    //        activeWeapon = taserGunModel;
    //        taserGunModel.SetActive(true); // Show the taser gun model
    //        crosshair.SetActive(true); // Show crosshair when weapon is equipped
    //    }
    //    else
    //    {
    //        crosshair.SetActive(false); // Hide crosshair if no weapon is equipped
    //    }

    //    Debug.Log($"Equipped: Weapon - {selectedWeapon}");
    //}

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
        // Switch to empty hands (press 4)
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EmptyHands(); // Make the player empty-handed
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            DeactivateAllModels();
        }
    }

    void SwitchWeapon()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        hasWeaponEquipped = true;

        // Activate the selected weapon
        if (selectedWeapon == "StunBaton")
        {
            activeWeapon = stunBatonModel;
            stunBatonModel.SetActive(true); // Show the stun baton model
            crosshair.SetActive(true); // Show crosshair when weapon is equipped
        }
        else if (selectedWeapon == "TaserGun")
        {
            activeWeapon = taserGunModel;
            taserGunModel.SetActive(true); // Show the taser gun model
            crosshair.SetActive(true); // Show crosshair when weapon is equipped
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
            crosshair.SetActive(false); // Hide crosshair when item is equipped
            Debug.Log("Equipped: " + selectedItem);
        }
        else if (selectedItem == "Coin")
        {
            activeItem = coinModel;
            coinModel.SetActive(true); // Show the coin model
            crosshair.SetActive(false); // Hide crosshair when item is equipped
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
            crosshair.SetActive(false); // Hide crosshair when gadget is equipped
            Debug.Log("Equipped: " + selectedGadget);
        }
        else if (selectedGadget == "Flashdrive")
        {
            activeGadget = flashdriveModel;
            flashdriveModel.SetActive(true); // Show the flashdrive model
            crosshair.SetActive(false); // Hide crosshair when gadget is equipped
            Debug.Log("Equipped: " + selectedGadget);
        }
    }

    void EmptyHands()
    {
        // Deactivate all models to make the player empty-handed
        DeactivateAllModels();
        Debug.Log("Equipped: Empty Hands");
    }

    void DeactivateAllModels()
    {
        hasWeaponEquipped = false;
        // Deactivate all models
        stunBatonModel.SetActive(false);
        taserGunModel.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        overrideKeyCardModel.SetActive(false);
        flashdriveModel.SetActive(false);

        // Hide crosshair when not holding anything
        crosshair.SetActive(false);
    }
}
