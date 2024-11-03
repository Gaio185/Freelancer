using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public string cardID; // Unique ID for each keycard (e.g., "LabDoor" or "MainHallDoor")

    public string GetCardID()
    {
        return cardID;
    }
}
