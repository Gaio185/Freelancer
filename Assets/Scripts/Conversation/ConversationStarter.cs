using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.EventSystems; // Required for managing UI Event Systems


public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private GameObject pressFText; // UI Text for "Press F to Talk"
    public GameObject conversationManager;
    public GameObject HUD;
    private bool isInConversation = false;
    private bool isPlayerInRange = false;

    private void Start()
    {
        // Hide the "Press F to Talk" text by default
        if (pressFText != null)
        {
            pressFText.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the conversation is active, and disable player movement if it is.
        if (isInConversation)
        {
            // Assuming the player has a PlayerController component that handles movement.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerController = player.GetComponent<PlayerMovement>();
                if (playerController != null)
                {
                    playerController.enabled = false; // Disable movement
                }
            }
        }
        else
        {
            // Re-enable player movement when conversation ends
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerController = player.GetComponent<PlayerMovement>();
                if (playerController != null)
                {
                    playerController.enabled = true; // Enable movement
                }
            }
        }

        // If player is still in range and presses 'F' again, restart the conversation
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isInConversation)
        {
            conversationManager.SetActive(true);
            HUD.SetActive(false);
            StartConversation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the trigger zone
            isPlayerInRange = true;

            // Show the "Press F to Talk" text when player enters the trigger
            if (pressFText != null && !isInConversation)
            {
                pressFText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the trigger zone
            isPlayerInRange = false;

            // Hide the "Press F to Talk" text when player exits the trigger
            if (pressFText != null)
            {
                pressFText.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isInConversation)
        {
            // If player is in range and presses 'F', start the conversation
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartConversation();
            }
        }
    }

    private void StartConversation()
    {
        // Start the conversation
        ConversationManager.Instance.StartConversation(myConversation);
        isInConversation = true; // Set the conversation state to true

        // Hide the "Press F to Talk" text during conversation
        if (pressFText != null)
        {
            pressFText.SetActive(false);
        }

        // Subscribe to the end conversation event
        ConversationManager.OnConversationEnded += EndConversation;

        // Unlock and show the cursor for conversation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void EndConversation()
    {
        // End the conversation and re-enable interaction
        isInConversation = false;
        ConversationManager.OnConversationEnded -= EndConversation;

        conversationManager.SetActive(false); // Hide the conversation UI
        HUD.SetActive(true);
        // Lock and hide the cursor after conversation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
