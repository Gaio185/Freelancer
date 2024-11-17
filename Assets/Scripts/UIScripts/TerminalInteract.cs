using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public GameObject computerInterface; // Reference to the terminal UI
    public float radius = 3f; // Interaction radius
    public LayerMask targetMask; // LayerMask for detecting terminals

    private GameObject player;
    public Player playerScript;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
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
                playerScript.movement.canMove = false; // Disable player movement
                playerScript.switchWeapon.disableTools = true; // Disable player tools
                playerScript.switchWeapon.DeactivateAllModels();
            }
        }
    }
}
