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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && extractionZone.isInRange)
        {
            if (player.movement.canExtract)
            {
                audioSourceVerify.PlayOneShot(audioSourceVerify.clip);
                Cursor.visible = true; // Show cursor when opening workspace
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                audioSourceDeny.PlayOneShot(audioSourceDeny.clip);
                Debug.Log("You can't extract yet.");
            }
        }
        
    }
}
