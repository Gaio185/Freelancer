using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    // UI Buttons for weapon selection
    public Button stunBatonButton;
    public Button taserGunButton;

    // UI Buttons for item selection
    public Button screwdriverButton;
    public Button coinButton;

    // UI Buttons for gadget selection
    public Button overrideKeyCardButton;
    public Button flashdriveButton;

    private void Start()
    {
        stunBatonButton.onClick.AddListener(() => SetWeapon("StunBaton"));
        taserGunButton.onClick.AddListener(() => SetWeapon("TaserGun"));
        screwdriverButton.onClick.AddListener(() => SetItem("Screwdriver"));
        coinButton.onClick.AddListener(() => SetItem("Coin"));
        overrideKeyCardButton.onClick.AddListener(() => SetGadget("OverrideKeyCard"));
        flashdriveButton.onClick.AddListener(() => SetGadget("Flashdrive"));
    }

    private void SetWeapon(string weapon)
    {
        PlayerPrefs.SetString("SelectedWeapon", weapon);
        Debug.Log($"Selected Weapon: {weapon}");
    }

    private void SetItem(string item)
    {
        PlayerPrefs.SetString("SelectedItem", item);
        Debug.Log($"Selected Item: {item}");
    }

    private void SetGadget(string gadget)
    {
        PlayerPrefs.SetString("SelectedGadget", gadget);
        Debug.Log($"Selected Gadget: {gadget}");
    }
}

