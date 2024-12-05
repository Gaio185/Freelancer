using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.EventSystems; // Required for managing UI Event Systems


public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public GameObject conversationManager;
    private bool isInConversation = false;
    private bool isPlayerInRange = false;
    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the trigger zone
            isPlayerInRange = true;

            // Show the "Press F to Talk" text when player enters the trigger
            if (player.interactPanel != null && !isInConversation)
            {
                player.interactionText.text = "Press F to Speak";
                player.interactPanel.SetActive(true);
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
            if (player.interactPanel != null)
            {
                player.interactPanel.SetActive(false);
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
                conversationManager.SetActive(true);
                player.HUD.SetActive(false);
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
        if (player.interactPanel != null)
        {
            player.interactPanel.SetActive(false);
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
        ConversationManager.OnConversationEnded -= EndConversation;

        conversationManager.SetActive(false); // Hide the conversation UI
        player.HUD.SetActive(true);
        // Lock and hide the cursor after conversation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isInConversation = false;
    }
}
