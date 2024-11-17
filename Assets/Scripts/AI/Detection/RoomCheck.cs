using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public bool isRestricted;
    private GameObject player;
    private Player playerScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           if(isRestricted)
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
    }
}
