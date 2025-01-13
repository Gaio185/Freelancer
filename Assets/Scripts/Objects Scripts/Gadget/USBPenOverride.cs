using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USBPenOverride : MonoBehaviour
{
    private Transform playerTransform;
    private bool playerInRange = false;  // Track if player is in range of a terminal
    private TerminalManagement targetTerminal; // Reference to the terminal within range
    public int useCount = 3;

    public GameObject overridePenDriveUI;
    public GameObject[] penUI;

    private void Start()
    {
        // Find and store the player's transform
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found.");
        }
    }

    private void OnEnable()
    {
        overridePenDriveUI.SetActive(true);
    }

    private void OnDisable()
    {
        overridePenDriveUI.SetActive(false);
    }
}
