using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeycardOverride : MonoBehaviour
{
    public int usesLeft = 3; // Limited uses for the override keycard

    public GameObject overrideKeycardUI;
    public TMP_Text countUI;

    public bool CanUse()
    {
        return usesLeft > 0;
    }

    public void Use()
    {
        if (CanUse())
        {
            usesLeft--;
            countUI.text = "x" + usesLeft;
            Debug.Log("Override Keycard used. Remaining uses: " + usesLeft);
        }
        else
        {
            Debug.Log("Override Keycard has no remaining uses.");
        }
    }

    private void OnEnable()
    {
        overrideKeycardUI.SetActive(true);
    }

    private void OnDisable()
    {
        overrideKeycardUI.SetActive(false);
    }
}
