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

    // Single spawn point for all tools
    public Transform generalSpawnPoint;
    public AnimationCurve curve;


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

    private void Start()
    {
        toolbar = FindObjectOfType<Toolbar>();
        DeactivateAllModels();
        hasWeapon = false;
        hasWeaponEquipped = false;

        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasWeapon)
        {
            SwitchWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchItem();
        }
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

            // Immediately move and activate the selected weapon
            if (selectedWeapon == "StunBaton")
            {
                StartCoroutine(SwitchToSpawnAndActivate(stunBatonHolder));
                crosshair.SetActive(true);
                SelectedWeaponUI(selectedWeapon, 0);
            }
            else if (selectedWeapon == "TaserGun")
            {
                StartCoroutine(SwitchToSpawnAndActivate(taserGunHolder));
                crosshair.SetActive(true);
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

        // Immediately move and activate the selected item
        if (selectedItem == "Screwdriver")
        {
            StartCoroutine(SwitchToSpawnAndActivate(screwdriverModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedItem, 1);
            Debug.Log("Equipped: " + selectedItem);
        }
        else if (selectedItem == "Coin")
        {
            StartCoroutine(SwitchToSpawnAndActivate(coinModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedItem, 1);
            Debug.Log("Equipped: " + selectedItem);
        }
    }

    void SwitchGadget()
    {
        DeactivateAllModels(); // Deactivate all models first
        string selectedGadget = PlayerPrefs.GetString("SelectedGadget", "OverrideKeyCard");

        hasWeaponEquipped = false;

        // Immediately move and activate the selected gadget
        if (selectedGadget == "OverrideKeyCard")
        {
            StartCoroutine(SwitchToSpawnAndActivate(overrideKeyCardModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedGadget, 2);
            Debug.Log("Equipped: " + selectedGadget);
        }
        else if (selectedGadget == "Flashdrive")
        {
            StartCoroutine(SwitchToSpawnAndActivate(flashdriveModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedGadget, 2);
            Debug.Log("Equipped: " + selectedGadget);
        }
    }

    void EmptyHands()
    {
        // Deactivate all models
        

        activeWeapon = null;
        activeItem = null;
        activeGadget = null;
        hasWeaponEquipped = false;
        Debug.Log("Equipped: Empty Hands");
    }

    // Function to deactivate all models
    public void DeactivateAllModels()
    {
        stunBatonHolder.SetActive(false);
        taserGunHolder.SetActive(false);
        screwdriverModel.SetActive(false);
        coinModel.SetActive(false);
        overrideKeyCardModel.SetActive(false);
        flashdriveModel.SetActive(false);
        ResetWeaponUI();
        hasWeaponEquipped = false;
        crosshair.SetActive(false);
    }

    // Coroutine to quickly move the item to the spawn point
    IEnumerator SwitchToSpawnAndActivate(GameObject item)
    {
        if (item != null)
        {
            // Start position is the spawn point
            Vector3 startPos = item.transform.localPosition + Vector3.left;
            Vector3 endPos = item.transform.localPosition;  // Final position (current position)
            float timeToMove = 0.45f; // Slightly slower movement duration
            float elapsedTime = 0f;

            item.SetActive(true); // Activate the item when starting the movement

            // Smooth interpolation using Lerp
            while (elapsedTime < timeToMove)
            {
                // Use Lerp to move smoothly from start to end
                //item.transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, elapsedTime / timeToMove));
                //item.transform.localPosition = Vector3.Lerp(startPos, endPos, (elapsedTime)/timeToMove);
                //item.transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.3f, Mathf.SmoothStep(0.0f, 1.3f, elapsedTime)));
                var normalizedProgress = elapsedTime / timeToMove; // 0-1
                var easing = curve.Evaluate(normalizedProgress);
                item.transform.localPosition = Vector3.Lerp(startPos, endPos, easing);

                elapsedTime += Time.deltaTime;  // Increase time
                yield return null;  // Wait for next frame
            }

            // Ensure that it finishes exactly at the final position
            //item.transform.localPosition = endPos;
        }
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

    // Unlock Weapon function to handle UI changes
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
