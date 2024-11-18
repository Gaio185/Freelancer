using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnlockSecretPassage : MonoBehaviour
{
    public GameObject pinUI; // Assign the PIN UI GameObject here
    public GameObject bookShelf; // Reference to the safe door object that should be destroyed
    public GameObject wall; // Reference to the wall object that should be destroyed
    private GameObject player; // Reference to the player GameObject for movement control
    public GameObject numPad; // Reference to the secret passage numpad


    private bool isPlayerNear = false;
    private bool isUIPinActive = false; // Track if the PIN UI is active

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player near the safe door");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            ClosePINUI();
            Debug.Log("Player left the safe door");
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            player.GetComponent<Switchweapon>().disableTools = true; // Disable player tools
            OpenPINUI(); // Open the PIN UI and manage cursor/camera
        }

        // Optional: Press Escape to close the UI
        if (isUIPinActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePINUI();
        }
    }

    public void UnlockPassage()
    {
        player.GetComponent<Switchweapon>().disableTools = false; // Enable player tools
        Destroy(bookShelf); // Destroy the safe door
        Destroy(wall); // Destroy the safe door
        Destroy(numPad); // Destroy the secret passage numpad
        ClosePINUI(); // Close the PIN UI and re-lock cursor after unlocking
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