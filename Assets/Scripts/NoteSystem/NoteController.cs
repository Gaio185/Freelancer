using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false); // Close note interface
            Cursor.visible = false; // Hide cursor
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            player.GetComponent<PlayerMovement>().canMove = true; // Enable player movement
        }
    }
}
