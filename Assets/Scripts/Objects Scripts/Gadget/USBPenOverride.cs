using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBPenOverride : MonoBehaviour
{
    public float interactionRange = 3f;  // Range within which the pen drive can interact with the terminal
    private Transform playerTransform;

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

    private void Update()
    {
        if (playerTransform == null) return;

        // Check if player is close enough and clicks Mouse1 (left mouse button)
        if (Input.GetMouseButtonDown(0))  // Left mouse button pressed
        {
            TryBypassTerminal();
        }
    }

    private void TryBypassTerminal()
    {
        // Check if player is within range of a terminal
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, interactionRange))
        {
            // Check if we hit a terminal and get the TerminalManagement component
            TerminalManagement terminal = hit.collider.GetComponent<TerminalManagement>();

            if (terminal != null)
            {
                terminal.BypassPassword(); // Call the terminal's bypass method
                Debug.Log("Terminal bypassed with USB Pen Drive.");
            }
            else
            {
                Debug.Log("No terminal detected within range.");
            }
        }
    }
}
