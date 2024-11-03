using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public bool isRestricted;
    private GameObject player;
    private PlayerMovement playerMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           if(isRestricted)
            {
                playerMovement.hasClearance = false;
                Debug.Log("Player does not have clearance");
            }
           else
            {
                playerMovement.hasClearance = true;
                Debug.Log("Player has clearance");
            }
        }
    }
}
