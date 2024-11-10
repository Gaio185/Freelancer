using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;   

public class CoolDownManager : MonoBehaviour
{
    public float taserCooldown;
    public float stunBatonCooldown;
    [HideInInspector] public float taserTimer;
    [HideInInspector] public float stunBatonTimer;
    public bool readyToUseTaser;
    public bool readyToUseStunBaton;

    public Slider taserSlider;
    public Slider stunBatonSlider;

    void Start()
    {
        taserTimer = 0f;
        stunBatonTimer = 0f;
        taserSlider.value = 1;
        stunBatonSlider.value = 1;
    }

    
    void Update()
    {
        taserTimer -= Time.deltaTime;
        if (taserTimer <= 0)
        {
            readyToUseTaser = true;
        }
        else if (taserTimer > 0)
        {
            taserSlider.value += (Time.deltaTime / taserTimer) * 0.2f;
        }

        stunBatonTimer -= Time.deltaTime;
        if (stunBatonTimer <= 0)
        {
            readyToUseStunBaton = true;
        }
        else if (taserTimer > 0)
        {
            stunBatonSlider.value += (Time.deltaTime / stunBatonTimer) * 0.2f;
        }
    }
}
