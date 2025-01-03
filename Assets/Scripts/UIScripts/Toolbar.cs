using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public List<GameObject> tools;
    public List<GameObject> Nums;
    private Player player;

    [SerializeField] public Color colorRef;

    public string selectedWeapon { get; private set;  }
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
                Transform border = tools[i].transform.GetChild(0);
                border.gameObject.GetComponent<Image>().color = Color.grey;
                Nums[0].GetComponent<TextMeshProUGUI>().color = Color.grey;
                tools[i].SetActive(true);
            }
        }

        //colorRef = GetBorderColor();
    }

    //private Color GetBorderColor()
    //{
    //    for (int i = 0; i < tools.Count; i++)
    //    {
    //        if (tools[i].activeSelf && i > 2)
    //        {
    //            Transform border = tools[i].transform.GetChild(0);
    //            return border.gameObject.GetComponent<Image>().color;
    //        }
    //    }

    //    return Color.white;
    //}
}
