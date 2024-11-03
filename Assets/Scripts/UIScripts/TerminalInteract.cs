using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public GameObject computerInterface; // Reference to the terminal UI
    public float radius = 3f; // Interaction radius
    public LayerMask targetMask; // LayerMask for detecting terminals

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length > 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                computerInterface.SetActive(true); // Show the terminal UI
                Cursor.visible = true; // Show cursor
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                player.GetComponent<PlayerMovement>().canMove = false; // Disable player movement
            }
        }
    }
}
