using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManagement : MonoBehaviour
{
    public TMP_InputField input; // Input field for password
    public GameObject passwordInterface; // UI for password input
    public GameObject workspaceInterface; // UI for workspace
    public TOAgent[] tOAgents; // Array of TOAgents (like cameras and sentries)

    public Button cameraOnButton; // Button to turn on camera
    public Button cameraOffButton; // Button to turn off camera
    public Button sentryOnButton; // Button to turn on sentry
    public Button sentryOffButton; // Button to turn off sentry

    [HideInInspector] public AudioSource audioSourceVerify; // Audio source for terminal sounds
    [HideInInspector] public AudioSource audioSourceDeny;

    private Player playerScript; // Reference to player script

    public bool canBeHacked;
    public bool isUnlocked;

    public string correctPassword; // Correct password for terminal access

    public void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSourceVerify = GameObject.FindWithTag("VerifyAccess").GetComponent<AudioSource>();
        audioSourceDeny = GameObject.FindWithTag("DenyAccess").GetComponent<AudioSource>();
    }

    public void CheckPassword()
    {
        if (input.text == correctPassword)
        {
            audioSourceVerify.PlayOneShot(audioSourceVerify.clip); // Play sound for successful login
            OpenWorkspace();
            input.text = ""; // Clear the input field after successful login
            isUnlocked = true;
            Debug.Log("Correct Password");
        }
        else
        {
            audioSourceDeny.PlayOneShot(audioSourceDeny.clip); // Play sound for successful login
            input.text = "";
            input.placeholder.GetComponent<TextMeshProUGUI>().text = "Incorrect Password"; 
        }
    }

    private void OpenWorkspace()
    {
        passwordInterface.SetActive(false); // Hide the password interface
        workspaceInterface.SetActive(true); // Show the workspace interface
        Cursor.visible = true; // Show cursor when opening workspace
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        playerScript.GetComponent<PlayerMovement>().canMove = false; // Allow player movement
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            playerScript.canPause = true; // Enable pause
            this.gameObject.SetActive(false); // Close terminal interface
            Cursor.visible = false; // Hide cursor
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            playerScript.movement.canMove = true; // Enable player movement
            playerScript.switchWeapon.disableTools = false; // Enable player tools  
            playerScript.HUD.SetActive(true); // Show HUD
        }

        // Manage button interactivity based on TOAgent states
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Camera")
            {
                if (tOAgents[i].stateMachine.currentState == TOStateId.On)
                {
                    cameraOnButton.interactable = false; // Disable "On" button if camera is on
                    cameraOffButton.interactable = true; // Enable "Off" button
                }
                else
                {
                    cameraOnButton.interactable = true; // Enable "On" button
                    cameraOffButton.interactable = false; // Disable "Off" button if camera is off
                }
            }
            else if (tOAgents[i].tag == "Sentry")
            {
                if (tOAgents[i].stateMachine.currentState == TOStateId.Off)
                {
                    sentryOnButton.interactable = true; // Enable "On" button if sentry is off
                    sentryOffButton.interactable = false; // Disable "Off" button
                }
                else
                {
                    sentryOnButton.interactable = false; // Disable "On" button if sentry is on
                    sentryOffButton.interactable = true; // Enable "Off" button
                }
            }
        }
    }

    // Methods for handling camera states
    public void CameraOnState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Camera")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.On); // Turn on camera
            }
        }
    }

    public void CameraOffState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Camera")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.Off); // Turn off camera
            }
        }
    }

    // Methods for handling sentry states
    public void SentryOnState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Sentry")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.On); // Turn on sentry
            }
        }
    }

    public void SentryOffState()
    {
        for (int i = 0; i < tOAgents.Length; i++)
        {
            if (tOAgents[i].tag == "Sentry")
            {
                tOAgents[i].stateMachine.ChangeState(TOStateId.Off); // Turn off sentry
            }
        }
    }
}
