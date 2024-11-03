using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBPenOverride : MonoBehaviour
{
    public float interactionRange = 3f; // Range within which the pen drive can interact with the terminal
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            TryBypassTerminal();
        }
    }

    void TryBypassTerminal()
    {
        // Cast a ray to detect if there's a terminal within range
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, interactionRange))
        {
            TerminalManagement terminal = hit.collider.GetComponent<TerminalManagement>();

            if (terminal != null)
            {
                terminal.BypassPassword(); // Bypass the terminal password
                Debug.Log("Terminal bypassed with Override Pen Drive.");
            }
            else
            {
                Debug.Log("No terminal detected within range.");
            }
        }
    }
}
