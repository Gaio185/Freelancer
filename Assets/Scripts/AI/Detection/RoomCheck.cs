using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class RoomCheck : MonoBehaviour
{
    public bool isRestricted;
    private GameObject player;
    private Player playerScript;

    // Reference to a TextMeshProUGUI element
    private TextMeshProUGUI clearanceStatusText;

    private void Start()
    {
        // Find player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        // If clearanceStatusText is not set in inspector, try to find it in the scene
        //if (clearanceStatusText == null)
        //{
        //    clearanceStatusText = GameObject.Find("ClearanceStatusText").GetComponent<TextMeshProUGUI>();
        //}

        //UpdateClearanceUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isRestricted)
            {
                playerScript.movement.hasClearance = false;
                Debug.Log("Player does not have clearance");
            }
            else
            {
                playerScript.movement.hasClearance = true;
                Debug.Log("Player has clearance");
            }

        }

        //UpdateClearanceUI();
    }

    private void UpdateClearanceUI()
    {
        // Change the UI text based on clearance status
        if (playerScript.movement.hasClearance && clearanceStatusText != null)
        {
            clearanceStatusText.text = "You have clearance!";
            clearanceStatusText.color = Color.green;  // Set text color to green
        }
        else if(clearanceStatusText != null)
        {
            clearanceStatusText.text = "No clearance!";
            clearanceStatusText.color = Color.red;    // Set text color to red
        }
    }
}
