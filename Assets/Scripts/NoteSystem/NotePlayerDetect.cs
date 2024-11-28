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
    public GameObject note; // Reference to the note

    private bool isInRange;
    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        noteInterface.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        interactPanel.SetActive(true); // Show the interact panel
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInRange)
        {
            HUD.SetActive(false); // Hide the HUD
            notesPanel.SetActive(true); // Hide the notes UI
            noteInterface.SetActive(true); // Show the note UI
            player.GetComponent<PlayerMovement>().canMove = false; // Disable player movement
            player.canPause = false; // Disable pause
        }

        if (Input.GetKeyDown(KeyCode.Escape) && notesPanel.activeSelf && noteInterface && isInRange && !player.canPause)
        {
            notesPanel.SetActive(false); // Close note interface
            noteInterface.SetActive(false); // Close note interface
            player.GetComponent<PlayerMovement>().canMove = true; // Enable player movement
            if (note != null)
            {
                player.GetComponent<Player>().notes.Add(note); // Add the note to the player's notes list
                interactPanel.SetActive(false); // Hide the interact panel
                note.SetActive(false); // Hide the note
                HUD.SetActive(true); // Show the HUD
                player.canPause = true; // Enable pause
            }
            else
            {
                HUD.SetActive(true); // Show the HUD
                player.canPause = true; // Enable pause
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactPanel.SetActive(true); // Show the interact panel
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPanel.SetActive(false); // Show the interact panel
            isInRange = false;
        }
    }

}
