using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SafeDoorController : MonoBehaviour
{
    public GameObject pinUI; // Assign the PIN UI GameObject here
    public GameObject safeDoor; // Reference to the safe door object that should be destroyed
    private Player player; // Reference to the player GameObject for movement control
    public GameObject safeContent; // Reference to the mission objective
    public GameObject numerario;

    public AudioSource audioSource;
    public AudioClip accessGranted;
    public AudioClip accessDenied;


    private bool isPlayerNear = false;
    private bool isUIPinActive = false; // Track if the PIN UI is active

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.interactionText.text = "Press F to Interact";
            isPlayerNear = true;
            Debug.Log("Player near the safe door");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.interactPanel.SetActive(true); // Show the interact panel
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player.interactPanel.SetActive(false); // Show the interact panel
            ClosePINUI();
            Debug.Log("Player left the safe door");
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSource = GameObject.FindWithTag("VerifyAccess").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            player.canPause = false; // Disable pause
            player.GetComponent<Switchweapon>().disableTools = true; // Enable player tools
            player.GetComponent<Switchweapon>().DeactivateAllModels();
            player.HUD.SetActive(false); // Hide HUD
            OpenPINUI(); // Open the PIN UI and manage cursor/camera
        }

        // Optional: Press Escape to close the UI
        if (isUIPinActive && Input.GetKeyDown(KeyCode.Escape) && isPlayerNear)
        {
            ClosePINUI();
            player.GetComponent<Switchweapon>().disableTools = false; // Enable player tools
            player.HUD.SetActive(true); // Show HUD
            player.canPause = true; // Enable pause
        }
    }

    public void UnlockSafe()
    {
        player.GetComponent<Switchweapon>().disableTools = false; // Enable player tools
        Destroy(safeDoor); // Destroy the safe door
        Destroy(numerario); // Destroy the numerario object
        ClosePINUI(); // Close the PIN UI and re-lock cursor after unlocking
        player.HUD.SetActive(true); // Show HUD
        player.interactPanel.SetActive(false); // Hide the interact panel
        safeContent.SetActive(true); // Activate the mission objective
        Debug.Log("Safe unlocked, door and numerario destroyed");
    }

    private void OpenPINUI()
    {
        pinUI.SetActive(true); // Show the PIN input UI
        UnlockCursor(); // Unlock cursor for PIN entry

        // Disable player movement
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = false; // Disable player movement
        }

        isUIPinActive = true;

        // Set focus to the first interactive element in your PIN UI
        EventSystem.current.SetSelectedGameObject(null); // Deselect everything first
        EventSystem.current.SetSelectedGameObject(pinUI.transform.GetChild(0).gameObject); // Select the first child (button/input)

        Debug.Log("PIN UI opened");
    }

    private void ClosePINUI()
    {
        pinUI.SetActive(false); // Hide the PIN input UI
        LockCursor(); // Lock cursor back to the center of the screen

        // Re-enable player movement
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().canMove = true; // Enable player movement
        }

        isUIPinActive = false;
        Debug.Log("PIN UI closed");
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI interaction
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor back to the center
        Cursor.visible = false;
    }
}
