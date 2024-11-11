using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionObjective : MonoBehaviour
{
    private GameObject playerRef;
    private PlayerMovement playerMovement;
    private bool isInRange;

    void Start()
    {
        playerRef = GameObject.Find("Player");
        playerMovement = playerRef.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.F) && isInRange)
        {
            playerMovement.canExtract = true;
            gameObject.SetActive(false);
        }
    }
}
