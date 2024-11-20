using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotePlayerDetect : MonoBehaviour
{
    public GameObject noteInterface; // Reference to the note UI
    public GameObject HUD; // Reference to the HUD
    public GameObject interactPanel; // Reference to the interact panel
    public GameObject notesPanel;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        noteInterface.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        interactPanel.SetActive(true); // Show the interact panel
        if (Input.GetKey(KeyCode.F))
        {
            HUD.SetActive(false); // Hide the HUD
            notesPanel.SetActive(true); // Hide the notes UI
            noteInterface.SetActive(true); // Show the note UI
            Cursor.visible = true; // Show cursor
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            player.GetComponent<PlayerMovement>().canMove = false; // Disable player movement
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            notesPanel.SetActive(false); // Close note interface
            noteInterface.SetActive(false); // Close note interface
            HUD.SetActive(true); // Show the HUD
            Cursor.visible = false; // Hide cursor
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            player.GetComponent<PlayerMovement>().canMove = true; // Enable player movement
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactPanel.SetActive(false); // Hide the interact panel
    }

}
