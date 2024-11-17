using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.EventSystems; // Required for managing UI Event Systems


public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private GameObject pressFText; // UI Text for "Press F to Talk"
    [SerializeField] private GameObject conversationUI; // Main conversation UI
    private bool isInConversation = false;
    private bool isPlayerInRange = false;

    private void Start()
    {
        // Hide the "Press F to Talk" text and ensure conversation UI is inactive
        if (pressFText != null)
        {
            pressFText.SetActive(false);
        }
        if (conversationUI != null)
        {
            conversationUI.SetActive(false);
        }
    }

    private void Update()
    {
        // Disable player movement during a conversation
        if (isInConversation)
        {
            DisablePlayerMovement();
        }
        else
        {
            EnablePlayerMovement();
        }

        // Check for interaction input when the player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isInConversation)
        {
            StartConversation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
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
            isPlayerInRange = false;
            if (pressFText != null)
            {
                pressFText.SetActive(false);
            }
        }
    }

    private void StartConversation()
    {
        if (ConversationManager.Instance == null)
        {
            Debug.LogError("ConversationManager.Instance is null!");
            return;
        }
        if (myConversation == null)
        {
            Debug.LogError("myConversation is not assigned!");
            return;
        }
        if (conversationUI == null)
        {
            Debug.LogError("ConversationUI is not assigned!");
            return;
        }

        // If all checks pass, proceed
        ConversationManager.Instance.StartConversation(myConversation);
        isInConversation = true;
        if (pressFText != null)
        {
            pressFText.SetActive(false);
        }
        if (conversationUI != null)
        {
            conversationUI.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ConversationManager.OnConversationEnded += EndConversation;
    }
    private void EndConversation()
    {
        isInConversation = false;

        // Re-hide the conversation UI
        if (conversationUI != null)
        {
            conversationUI.SetActive(false);
        }

        // Lock cursor and reset UI input state
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Disable EventSystem to prevent UI overlap
        EventSystem.current.enabled = false;

        ConversationManager.OnConversationEnded -= EndConversation;
    }

    private void DisablePlayerMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var playerController = player.GetComponent<PlayerMovement>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }
    }

    private void EnablePlayerMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var playerController = player.GetComponent<PlayerMovement>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }
}
