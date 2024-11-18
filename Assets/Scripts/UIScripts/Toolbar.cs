using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public List<GameObject> tools;

    // Start is called before the first frame update
    void Start()
    {
        string selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        string selectedItem = PlayerPrefs.GetString("SelectedItem", "Screwdriver");
        string selectedGadget = PlayerPrefs.GetString("SelectedGadget", "OverrideKeyCard");

        for (int i = 0; i < tools.Count; i++)
        {
            if (selectedWeapon == tools[i].name || selectedItem == tools[i].name || selectedGadget == tools[i].name)
            {
                tools[i].SetActive(true);
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
