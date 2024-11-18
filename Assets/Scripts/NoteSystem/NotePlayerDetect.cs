using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlayerDetect : MonoBehaviour
{
    public GameObject noteInterface; // Reference to the note UI
    public float radius = 3f; // Interaction radius
    public LayerMask targetMask; // LayerMask for detecting notes

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        noteInterface.SetActive(false);
    }

    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length > 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                noteInterface.SetActive(true); // Show the note UI
                Cursor.visible = true; // Show cursor
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                player.GetComponent<PlayerMovement>().canMove = false; // Disable player movement
            }
        }
    }

}
