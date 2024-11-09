using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class USBPenOverride : MonoBehaviour
{
    public float interactionRange = 3f; // Range within which the pen drive can interact with the terminal
    private bool playerInRange = false;  // Track if player is in range of a terminal
    private TerminalManagement targetTerminal; // Reference to the terminal within range
    private float useCount = 3;

    public GameObject overridePenDriveUI;
    public TMP_Text countUI;

    private void Update()
    {
        // If player is in range and clicks Mouse1, try to bypass terminal
        if (playerInRange && Input.GetMouseButtonDown(0)) // 0 = Mouse1
        {
            TryBypassTerminal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if we entered range of a terminal
        if (other.CompareTag("Terminal"))
        {
            playerInRange = true;
            targetTerminal = other.GetComponent<TerminalManagement>(); // Reference to the terminal
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset when leaving terminal range
        if (other.CompareTag("Terminal"))
        {
            playerInRange = false;
            targetTerminal = null;
        }
    }

    private void TryBypassTerminal()
    {
        // Only attempt to bypass if a terminal is in range
        if (targetTerminal != null)
        {
            --useCount;
            countUI.text = "x" + useCount;
            targetTerminal.BypassPassword(); // Bypass the terminal password
            Debug.Log("Terminal bypassed with Override Pen Drive.");
        }
        else
        {
            Debug.Log("No terminal detected within range.");
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
