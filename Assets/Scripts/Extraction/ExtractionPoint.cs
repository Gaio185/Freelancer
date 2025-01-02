using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionPoint : MonoBehaviour
{
    private Player player;
    public ExtractionZone extractionZone;
    private AudioSource audioSourceVerify;
    private AudioSource audioSourceDeny;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSourceVerify = GameObject.FindWithTag("VerifyAccess").GetComponent<AudioSource>();
        audioSourceDeny = GameObject.FindWithTag("DenyAccess").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && extractionZone.isInRange)
        {
            if (player.movement.canExtract)
            {
                audioSourceVerify.PlayOneShot(audioSourceVerify.clip);
                Cursor.visible = true; // Show cursor when opening workspace
                Cursor.lockState = CursorLockMode.None; // Unlock cursor

                // Trigger Victory and load the Victory Screen
                TriggerVictory();
            }
            else
            {
                audioSourceDeny.PlayOneShot(audioSourceDeny.clip);
                Debug.Log("You can't extract yet.");
            }
        }
    }

    void TriggerVictory()
    {
        // Load the Victory Scene after extraction
        SceneManager.LoadScene("VictoryScene"); // Change "VictoryScene" to the actual name of your victory scene
    }
}
