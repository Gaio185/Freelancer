using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionPoint : MonoBehaviour
{
    private GameObject playerRef;
    private PlayerMovement playerMovement;
    public ExtractionZone extractionZone;

    void Start()
    {
        playerRef = GameObject.Find("Player");
        playerMovement = playerRef.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && extractionZone.isInRange)
        {
            if (playerMovement.canExtract)
            {
                Cursor.visible = true; // Show cursor when opening workspace
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("You can't extract yet.");
            }
        }
        
    }
}
