using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject stunBatonModel;  // Assign this to the StunBaton model in the WeaponHolder
    public GameObject taserGunModel;   // Assign this to the TaserGun model in the WeaponHolder

    void Start()
    {
        EquipSelectedWeapon();
    }

    void EquipSelectedWeapon()
    {
        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");

        // Deactivate both weapons first
        stunBatonModel.SetActive(false);
        taserGunModel.SetActive(false);

        // Activate only the selected weapon
        if (selectedWeapon == "TaserGun")
        {
            taserGunModel.SetActive(true);
        }
        else if (selectedWeapon == "StunBaton")
        {
            stunBatonModel.SetActive(true);
        }
    }
}
