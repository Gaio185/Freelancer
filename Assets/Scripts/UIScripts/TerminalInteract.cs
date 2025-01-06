using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public GameObject computerInterface; // Reference to the terminal UI

    public Player playerScript;

    public USBPenOverride overridePenDrive;

    public TerminalCollisionCheck terminalCollisionCheck;

    private AudioSource audioSourceVerify;
    private AudioSource audioSourceDeny;


    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSourceVerify = GameObject.FindWithTag("VerifyAccess").GetComponent<AudioSource>();
        audioSourceDeny = GameObject.FindWithTag("DenyAccess").GetComponent<AudioSource>(); 
    }

    void Update()
    {

        if (terminalCollisionCheck.isInRange)
        {
            TerminalManagement terminalManagement = computerInterface.GetComponent<TerminalManagement>();

            terminalCollisionCheck.player.interactPanel.SetActive(true); // Show the interact panel
            if (Input.GetKeyDown(KeyCode.F) && !overridePenDrive.enabled)
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
            

            if (Input.GetKeyDown(KeyCode.F) && overridePenDrive.enabled)
            {
                TerminalManagement terminal = computerInterface.GetComponent<TerminalManagement>();
                if (!terminal.isUnlocked && terminal.canBeHacked)
                {
                    --overridePenDrive.useCount;
                    overridePenDrive.penUI[overridePenDrive.useCount].SetActive(false);
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
                    audioSourceVerify.PlayOneShot(audioSourceVerify.clip); // Play sound for successful login
                    terminal.isUnlocked = true;
                    Debug.Log("Terminal bypassed with USB Pen Drive.");
                }
                else if(!terminal.isUnlocked)
                {
                    audioSourceDeny.PlayOneShot(audioSourceDeny.clip); 
                }
            }
        }
    }
}
