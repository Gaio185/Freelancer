using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public List<GameObject> tools;
    private Player player;

    private string selectedWeapon;
    private string selectedItem;
    private string selectedGadget;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        selectedWeapon = PlayerPrefs.GetString("SelectedWeapon", "StunBaton");
        selectedItem = PlayerPrefs.GetString("SelectedItem", "Screwdriver");
        selectedGadget = PlayerPrefs.GetString("SelectedGadget", "OverrideKeyCard");

        for (int i = 0; i < tools.Count; i++)
        {
            if (selectedItem == tools[i].name || selectedGadget == tools[i].name)
            {
                tools[i].SetActive(true);
            }else if(selectedWeapon == tools[i].name)
            {
                tools[i].GetComponent<Image>().color = Color.grey;
                tools[i].SetActive(true);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Switchweapon>().hasWeapon)
        {
            for (int i = 0; i < tools.Count; i++)
            {
                if (tools[i].name == selectedWeapon)
                {
                    tools[i].GetComponent <Image>().color = Color.white;
                }
            }
        }
    }
}
