using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoor : MonoBehaviour
{
    [SerializeField] private Transform ventSpawnPoint;  // Destination for teleportation (can be an empty GameObject)

    [SerializeField] private VentDoor ventToTravelTo;

    private Player player; // Manually assign the player here

    private bool ventIsOpen = false; // Track whether the vent is opened or not

    private bool isInRange;

    [SerializeField] private GameObject screwdriver;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip unlockVentSound;
    [SerializeField] private AudioClip enterVentSound;

    // Initialize the MeshRenderer reference
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && screwdriver.activeSelf && !ventIsOpen)
        {
            isInRange = true;
            player.interactionText.text = "Press F to Open Vent";
            player.interactPanel.SetActive(true);
        }
        else if(other.gameObject.tag == "Player" && ventIsOpen)
        {
            isInRange = true;
            player.interactionText.text = "Press F to Use Vent";
            player.interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            player.interactPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInRange && !ventIsOpen)
        {
            OpenVent();
            player.interactionText.text = "Press F to Use Vent";
        }
        else if (Input.GetKeyDown(KeyCode.F) && isInRange && ventIsOpen)
        {
            EnterVent();
        }

        if (isInRange && !ventIsOpen && screwdriver.activeSelf)
        {
            player.interactionText.text = "Press F to Open Vent";
            player.interactPanel.SetActive(true);
        }
        else if (isInRange && !ventIsOpen && !screwdriver.activeSelf)
        {
            player.interactPanel.SetActive(false);
        }
    }

    // Method to open the vent (called when screwdriver is used)
    public void OpenVent()
    {
        if (!ventIsOpen)
        {
            audioSource.Stop();
            audioSource.clip = unlockVentSound;
            audioSource.Play();
            ventIsOpen = true;
            //DeactivateVentMesh(); // Deactivate the MeshRenderer
        }
    }

    // Method to handle teleportation when 'F' is pressed
    public void EnterVent()
    {
        if (player != null && ventToTravelTo.ventSpawnPoint != null)
        {
            ventToTravelTo.ventIsOpen = true;

            // Temporarily disable CharacterController or Rigidbody if present
            CharacterController characterController = player.gameObject.GetComponent<CharacterController>();

            if (characterController != null) characterController.enabled = false;

            // Teleport the player to the destination
            player.gameObject.transform.position = ventToTravelTo.ventSpawnPoint.position;

            audioSource.Stop();
            audioSource.clip = enterVentSound;
            audioSource.Play();

            // Reactivate CharacterController or Rigidbody
            if (characterController != null) characterController.enabled = true;

            player.interactPanel.SetActive(false);
            isInRange = false;
            Debug.Log("Player teleported to destination: " + ventToTravelTo.ventSpawnPoint.position);
        }
        else
        {
            Debug.LogError("Player or destination is not assigned in VentDoor script.");
        }
    }

    // Method to check if vent is open
    public bool IsVentOpen()
    {
        return ventIsOpen;
    }
}
