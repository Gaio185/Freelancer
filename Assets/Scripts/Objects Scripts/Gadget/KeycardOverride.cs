using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardOverride : MonoBehaviour
{
    public int usesLeft = 3; // Limited uses for the override keycard

    public bool CanUse()
    {
        return usesLeft > 0;
    }

    public void Use()
    {
        if (CanUse())
        {
            usesLeft--;
            Debug.Log("Override Keycard used. Remaining uses: " + usesLeft);
        }
        else
        {
            Debug.Log("Override Keycard has no remaining uses.");
        }
    }


}
