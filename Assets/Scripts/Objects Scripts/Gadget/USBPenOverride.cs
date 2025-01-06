using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USBPenOverride : MonoBehaviour
{
    public float interactionRange = 3f;  // Range within which the pen drive can interact with the terminal
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

    //private void Update()
    //{
    //    if (playerTransform == null) return;

    //    ////Check if player is close enough and clicks Mouse1 (left mouse button)
    //    //if (Input.GetMouseButtonDown(0))  // Left mouse button pressed
    //    //{
    //    //    TryBypassTerminal();
    //    //}
    //}

    //private void TryBypassTerminal()
    //{
    //    // Check if player is within range of a terminal
    //    RaycastHit hit;
    //    if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, interactionRange))
    //    {

    //        // Check if we hit a terminal and get the TerminalManagement component
    //        TerminalInteract terminal = hit.collider.GetComponent<TerminalInteract>();

    //        if (terminal != null)
    //        {
    //            --useCount;
    //            countUI.text = "x" + useCount;
    //            TerminalManagement computerInterface = terminal.computerInterface.GetComponent<TerminalManagement>();
    //            if (!computerInterface.isUnlocked)
    //            {
    //                --useCount;
    //                countUI.text = "x" + useCount;
    //                computerInterface.BypassPassword(); // Call the terminal's bypass method
    //                Debug.Log("Terminal bypassed with USB Pen Drive.");
    //            }

    //        }
    //        else
    //        {
    //            Debug.Log("No terminal detected within range.");
    //        }

    //        //targetTerminal.BypassPassword(); // Bypass the terminal password
    //        //Debug.Log("Terminal bypassed with Override Pen Drive.");
    //    }
    //    else
    //    {
    //        Debug.Log("No terminal detected within range.");

    //    }
    //}

    private void OnEnable()
    {
        overridePenDriveUI.SetActive(true);
    }

    private void OnDisable()
    {
        overridePenDriveUI.SetActive(false);
    }
}
