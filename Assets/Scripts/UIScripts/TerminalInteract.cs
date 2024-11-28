using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public GameObject computerInterface; // Reference to the terminal UI

    public Player playerScript;

    private GameObject overridePenDrive;

    public TerminalCollisionCheck terminalCollisionCheck;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {

        if (terminalCollisionCheck.isInRange)
        {
            TerminalManagement terminalManagement = computerInterface.GetComponent<TerminalManagement>();

            terminalCollisionCheck.interactPanel.SetActive(true); // Show the interact panel
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerScript.canPause = false; // Disable pause
                playerScript.HUD.SetActive(false); // Hide the HUD
                computerInterface.SetActive(true); // Show the terminal UI
                Cursor.visible = true; // Show cursor
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                playerScript.movement.canMove = false; // Disable player movement
                playerScript.switchWeapon.disableTools = true; // Disable player tools
                playerScript.switchWeapon.DeactivateAllModels();
                computerInterface.GetComponent<TerminalManagement>().isUnlocked = true;
            }
            

            if (Input.GetMouseButtonDown(0) && overridePenDrive.activeSelf)
            {
                USBPenOverride usbPen = overridePenDrive.GetComponent<USBPenOverride>();
                TerminalManagement terminal = computerInterface.GetComponent<TerminalManagement>();
                if (!terminal.isUnlocked && terminal.canBeHacked)
                {
                    --usbPen.useCount;
                    usbPen.countUI.text = "x" + usbPen.useCount;
                    terminal.passwordInterface.SetActive(false); // Hide the password interface
                    terminal.workspaceInterface.SetActive(true); // Show the workspace interface
                    playerScript.canPause = false; // Disable pause
                    playerScript.HUD.SetActive(false); // Hide the HUD
                    computerInterface.SetActive(true); // Show the terminal UI
                    Cursor.visible = true; // Show cursor
                    Cursor.lockState = CursorLockMode.None; // Unlock cursor
                    playerScript.movement.canMove = false; // Disable player movement
                    playerScript.switchWeapon.disableTools = true; // Disable player tools
                    playerScript.switchWeapon.DeactivateAllModels();
                    terminal.isUnlocked = true;
                    Debug.Log("Terminal bypassed with USB Pen Drive.");
                }
            }
            else if(overridePenDrive == null)
            {
                overridePenDrive = GameObject.FindWithTag("USBPen");
            }
        }
    }
}
