using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeycardOverride : MonoBehaviour
{
    [SerializeField] private int usesLeft = 3; // Limited uses for the override keycard

    [SerializeField] private GameObject overrideKeycardUI;
    [SerializeField] private List<GameObject> keycards;

    public bool CanUse()
    {
        return usesLeft > 0;
    }

    public void Use()
    {
        if (CanUse())
        {
            --usesLeft;
            keycards[usesLeft].SetActive(false);
            keycards.RemoveAt(usesLeft);
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
