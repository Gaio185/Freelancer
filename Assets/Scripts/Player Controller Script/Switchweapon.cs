using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Switchweapon : MonoBehaviour
{
    // Weapon models
    public GameObject stunBatonModel;
    public GameObject taserGunModel;
    public GameObject stunBatonHolder;
    public GameObject taserGunHolder;

    // Item models
    public GameObject screwdriverModel;
    public GameObject coinModel;

    // Gadget models
    public GameObject overrideKeyCardModel;
    public GameObject flashdriveModel;

    public bool hasWeapon;
    public bool hasWeaponEquipped;
    public bool disableTools;

    // Keep track of active objects
    private GameObject activeWeapon;
    private GameObject activeItem;
    private GameObject activeGadget;

    // Crosshair reference
    public GameObject crosshair;

    public Toolbar toolbar { get; private set; }

    public float radius = 3f;
    public LayerMask targetMask;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        toolbar = FindObjectOfType<Toolbar>();
        DeactivateAllModels();
        hasWeapon = false;
        hasWeaponEquipped = false;
        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        Debug.Log("A string selectedWeapon contem: " + selectedWeapon);

        if (selectedWeapon == "StunBaton")
        {
            stunBatonModel.SetActive(true);
            taserGunModel.SetActive(false);
        }
        else if (selectedWeapon == "TaserGun")
        {
            taserGunModel.SetActive(true);
            stunBatonModel.SetActive(false);
        }
    }

    void Update()
    {
        if (!disableTools)
        {
            HandleSwitching(); // Check for input to switch items
        }
    }

    void HandleSwitching()
    {
        // Switch to weapons (press 1)
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasWeapon)
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
        else if (Input.GetKeyDown(KeyCode.H))
        {
            EmptyHands();
        }
    }

    void SwitchWeapon()
    {
        DeactivateAllModels(); // Deactivate all models first
        if (hasWeapon)
        {
            hasWeaponEquipped = true;

            string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");

            // Activate the selected weapon
            if (selectedWeapon == "StunBaton")
            {
                activeWeapon = stunBatonHolder;
                stunBatonHolder.SetActive(true); // Show the stun baton model
                crosshair.SetActive(true); // Show crosshair when weapon is equipped
                SelectedWeaponUI(selectedWeapon, 0);
            }
            else if (selectedWeapon == "TaserGun")
            {
                activeWeapon = taserGunHolder;
                taserGunHolder.SetActive(true); // Show the taser gun model
                crosshair.SetActive(true); // Show crosshair when weapon is equipped
                SelectedWeaponUI(selectedWeapon, 0);
            }

            Debug.Log($"Equipped: Weapon - {selectedWeapon}");
        }
        
    }

    void SwitchItem()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedItem = PlayerPrefs.GetString("SelectedItem", "Screwdriver");

        hasWeaponEquipped = false;

        // Activate the selected item
        if (selectedItem == "Screwdriver")
        {
            activeItem = screwdriverModel;
            screwdriverModel.SetActive(true); // Show the screwdriver model
            crosshair.SetActive(false); // Hide crosshair when item is equipped
            SelectedWeaponUI(selectedItem, 1);
            Debug.Log("Equipped: " + selectedItem);
        }
        else if (selectedItem == "Coin")
        {
            activeItem = coinModel;
            coinModel.SetActive(true); // Show the coin model
            crosshair.SetActive(false); // Hide crosshair when item is equipped
            SelectedWeaponUI(selectedItem, 1);
            Debug.Log("Equipped: " + selectedItem);
        }
    }

    void SwitchGadget()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedGadget = PlayerPrefs.GetString("SelectedGadget", "OverrideKeyCard");

        hasWeaponEquipped = false;
        // Activate the selected gadget
        if (selectedGadget == "OverrideKeyCard")
        {
            activeGadget = overrideKeyCardModel;
            overrideKeyCardModel.SetActive(true); // Show the override keycard model
            crosshair.SetActive(false); // Hide crosshair when gadget is equipped
            SelectedWeaponUI(selectedGadget, 2);
            Debug.Log("Equipped: " + selectedGadget);
        }
        else if (selectedGadget == "Flashdrive")
        {
            activeGadget = flashdriveModel;
            flashdriveModel.SetActive(true); // Show the flashdrive model
            crosshair.SetActive(false); // Hide crosshair when gadget is equipped
            SelectedWeaponUI(selectedGadget, 2);
            Debug.Log("Equipped: " + selectedGadget);
        }
    }

    void EmptyHands()
    {
        // Deactivate all models to make the player empty-handed
        DeactivateAllModels();
        hasWeaponEquipped = false;
        Debug.Log("Equipped: Empty Hands");
    }

    public void DeactivateAllModels()
    {
        // Deactivate all models
        stunBatonHolder.SetActive(false);
        taserGunHolder.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        overrideKeyCardModel.SetActive(false);
        flashdriveModel.SetActive(false);
        ResetWeaponUI();
        hasWeaponEquipped = false;
        // Hide crosshair when not holding anything
        crosshair.SetActive(false);
    }

    private void SelectedWeaponUI(string selectedItem, int numKey)
    {
        for (int i = 0; i < toolbar.tools.Count; i++)
        {
            if (selectedItem == toolbar.tools[i].name)
            {
                Transform border = toolbar.tools[i].transform.GetChild(0);
                border.gameObject.GetComponent<Image>().color = Color.white;
            }
        }

        toolbar.Nums[numKey].GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    private void ResetWeaponUI()
    {
        for (int i = 0; i < toolbar.tools.Count; i++)
        {
            if (!hasWeapon && i < 2)
            {
                i = 2;
            }
            Transform border = toolbar.tools[i].transform.GetChild(0);
            border.gameObject.GetComponent<Image>().color = toolbar.colorRef;
        }

        for (int i = 0; i < toolbar.Nums.Count; i++)
        {
            if (!hasWeapon && i < 1)
            {
                i = 1;
            }
            toolbar.Nums[i].GetComponent<TextMeshProUGUI>().color = toolbar.colorRef;
        }
    }

    public void UnlockWeapon()
    {
        for (int i = 0; i < toolbar.tools.Count; i++)
        {
            if (toolbar.tools[i].name == toolbar.selectedWeapon)
            {
                toolbar.tools[i].GetComponent<Image>().color = Color.white;
                Transform border = toolbar.tools[i].transform.GetChild(0);
                border.gameObject.GetComponent<Image>().color = toolbar.colorRef;
                toolbar.Nums[0].GetComponent<TextMeshProUGUI>().color = toolbar.colorRef;
            }
        }
    }
}
