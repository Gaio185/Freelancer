using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public GameObject computerInterface; // Reference to the terminal UI

    public Player playerScript;

    public TerminalCollisionCheck terminalCollisionCheck;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {

        if (terminalCollisionCheck.isInRange)
        {
            terminalCollisionCheck.interactPanel.SetActive(true); // Show the interact panel
            if (Input.GetKey(KeyCode.F))
            {
                playerScript.canPause = false; // Disable pause
                playerScript.HUD.SetActive(false); // Hide the HUD
                computerInterface.SetActive(true); // Show the terminal UI
                Cursor.visible = true; // Show cursor
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                playerScript.movement.canMove = false; // Disable player movement
                playerScript.switchWeapon.disableTools = true; // Disable player tools
                playerScript.switchWeapon.DeactivateAllModels();
            }
        }
    }
}
