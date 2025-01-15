using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None,
    Weapon,
    Item,
    Gadget
}

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

    private ItemType currentItemType = ItemType.None;  // Track which item is currently equipped

    // Crosshair reference
    public GameObject crosshair;

    public Toolbar toolbar { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip useTool;

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasWeapon)  // Press 1 to toggle weapon
        {
            ToggleItem(ItemType.Weapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // Press 2 to toggle item
        {
            ToggleItem(ItemType.Item);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))  // Press 3 to toggle gadget
        {
            ToggleItem(ItemType.Gadget);
        }
        else if (Input.GetKeyDown(KeyCode.H))  // Press H to empty hands
        {
            EmptyHands();
        }
    }

    // Toggle visibility of the currently equipped item
    void ToggleItem(ItemType itemType)
    {
        if (currentItemType == itemType)
        {
            DeactivateAllModels();  // If the same item is toggled, deactivate it (holster it)
            currentItemType = ItemType.None;
        }
        else
        {
            currentItemType = itemType;
            DeactivateAllModels();  // Deactivate any previous item
            ActivateItem(itemType);  // Activate the new item
        }
    }

    void ActivateItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                ActivateWeapon();
                break;
            case ItemType.Item:
                ActivateItemTools();
                break;
            case ItemType.Gadget:
                ActivateGadget();
                break;
            default:
                break;
        }
    }

    void ActivateWeapon()
    {
        audioSource.Stop();
        audioSource.clip = useTool;
        audioSource.Play();

        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        hasWeaponEquipped = true;

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

        Debug.Log($"Weapon Equipped: {selectedWeapon}");
    }

    void ActivateItemTools()
    {
        audioSource.Stop();
        audioSource.clip = useTool;
        audioSource.Play();

        string selectedItem = PlayerPrefs.GetString("SelectedItem", "Screwdriver");

        if (selectedItem == "Screwdriver")
        {
            StartCoroutine(SwitchToSpawnAndActivate(screwdriverModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedItem, 1);
        }
        else if (selectedItem == "Coin")
        {
            StartCoroutine(SwitchToSpawnAndActivate(coinModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedItem, 1);
        }

        Debug.Log($"Item Equipped: {selectedItem}");
    }

    void ActivateGadget()
    {
        audioSource.Stop();
        audioSource.clip = useTool;
        audioSource.Play();

        string selectedGadget = PlayerPrefs.GetString("SelectedGadget", "OverrideKeyCard");

        if (selectedGadget == "OverrideKeyCard")
        {
            StartCoroutine(SwitchToSpawnAndActivate(overrideKeyCardModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedGadget, 2);
        }
        else if (selectedGadget == "Flashdrive")
        {
            StartCoroutine(SwitchToSpawnAndActivate(flashdriveModel));
            crosshair.SetActive(false);
            SelectedWeaponUI(selectedGadget, 2);
        }

        Debug.Log($"Gadget Equipped: {selectedGadget}");
    }

    void EmptyHands()
    {
        audioSource.Stop();
        DeactivateAllModels();
        currentItemType = ItemType.None;
        Debug.Log("Equipped: Empty Hands");
    }

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
            Vector3 startPos = item.transform.localPosition + Vector3.left;
            Vector3 endPos = item.transform.localPosition;
            float timeToMove = 0.45f;
            float elapsedTime = 0f;

            item.SetActive(true); // Activate the item when starting the movement

            while (elapsedTime < timeToMove)
            {
                var normalizedProgress = elapsedTime / timeToMove;
                var easing = curve.Evaluate(normalizedProgress);
                item.transform.localPosition = Vector3.Lerp(startPos, endPos, easing);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
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

    // Restored UnlockWeapon function
    public void UnlockWeapon()
    {
        // Update the UI for the unlocked weapon
        for (int i = 0; i < toolbar.tools.Count; i++)
        {
            if (toolbar.tools[i].name == toolbar.selectedWeapon)
            {
                // Change the UI color to indicate that the weapon is unlocked
                toolbar.tools[i].GetComponent<Image>().color = Color.white;
                Transform border = toolbar.tools[i].transform.GetChild(0);
                border.gameObject.GetComponent<Image>().color = toolbar.colorRef;
                toolbar.Nums[0].GetComponent<TextMeshProUGUI>().color = toolbar.colorRef;
            }
        }
    }
}
