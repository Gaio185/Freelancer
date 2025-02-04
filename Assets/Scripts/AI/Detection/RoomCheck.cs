using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public enum Floor
{
    Bottom,
    Middle,
    Top
}

public class RoomCheck : MonoBehaviour
{
    public bool isRestricted;
    private GameObject player;
    private Player playerScript;
    [SerializeField] private Floor floor;

    private void Start()
    {
        // Find player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
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
            playerScript.currentFloor = floor;
        }
    }
}
