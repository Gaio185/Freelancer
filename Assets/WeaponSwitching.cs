using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; // Default is weapon 0
    public bool isUnarmed = false; // State to check if player is unarmed

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Mouse Scroll Forward
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount)
            {
                selectedWeapon = -1; // No weapon selected
            }
            else
            {
                selectedWeapon++;
            }
        }

        // Mouse Scroll Backward
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= -1)
            {
                selectedWeapon = transform.childCount - 1; // Go to the last weapon
            }
            else
            {
                selectedWeapon--;
            }
        }

        // Check for keyboard input (1, 2 for weapon selection)
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        // Change: Use '3' key to become unarmed
        if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedWeapon = -1; // Set to unarmed
        }

        // Select the weapon if it has changed
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        // If unarmed (selectedWeapon = -1), deactivate all weapons
        if (selectedWeapon == -1)
        {
            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(false);
            }
            isUnarmed = true; // Set unarmed state
            return; // Exit the function since no weapon is selected
        }

        // Otherwise, activate the correct weapon
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        isUnarmed = false; // Set the state to armed
    }
}
