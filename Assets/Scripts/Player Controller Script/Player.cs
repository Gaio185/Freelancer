using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerMovement movement;
    public Switchweapon switchWeapon;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        switchWeapon = GetComponent<Switchweapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
