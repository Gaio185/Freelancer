using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerMovement movement;
    public Switchweapon switchWeapon;
    public List<Keycard> keycards;
    public List<GameObject> keycardUI;


    // Start is called before the first frame update
    void Start()
    {
        keycards = new List<Keycard>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        switchWeapon = GetComponent<Switchweapon>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < keycards.Count; i++)
        {
            for(int j = 0; j < keycardUI.Count; j++)
            {
                if(keycards[i].GetDivisionType().ToString() == keycardUI[j].name)
                {
                    keycardUI[j].SetActive(true);
                }
            }
        }
    }
}
