using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeycardReader : MonoBehaviour
{
    public Door controlledDoor;  // The door this reader unlocks
    public DivisionType requiredDivisionType;  // The required division type for this reader
    private Player player;
    private AudioSource audioSource;

    public LayerMask targetMask;

    public AudioClip accessGranted;
    public AudioClip accessDenied;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSource = GameObject.FindWithTag("VerifyAccess").GetComponent<AudioSource>();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown((KeyCode.F)) && !controlledDoor.isUnlocked)  // Use mouse click for interaction
    //    {
    //        TryUnlock();
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown((KeyCode.F)) && !controlledDoor.isUnlocked)  // Use mouse click for interaction
        {
            TryUnlock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player.interactionText.text = "Press F to Interact";
        player.interactPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        player.interactPanel.SetActive(false);
    }

    private void TryUnlock()
    {
        // Check for KeycardOverride (this will unlock the door regardless of the division type)
        KeycardOverride overrideKeycard = player.GetComponent<Switchweapon>().overrideKeyCardModel.GetComponent<KeycardOverride>();
        if (overrideKeycard != null && overrideKeycard.CanUse() && overrideKeycard.gameObject.activeSelf && requiredDivisionType != DivisionType.CEO
            && requiredDivisionType != DivisionType.Security)
        {
            overrideKeycard.Use();
            controlledDoor?.Unlock();  // Unlock the controlled door
            audioSource.PlayOneShot(accessGranted);
            Debug.Log("Door unlocked with override keycard.");
            return;
        }

        // Check for a regular Keycard
        for (int i = 0; i < player.GetComponent<Player>().keycards.Count; i++)
        {
            Keycard keycard = player.keycards[i];
            if (keycard.GetDivisionType() == requiredDivisionType)
            {
                controlledDoor?.Unlock();  // Unlock the controlled door
                audioSource.PlayOneShot(accessGranted);
                Debug.Log("Door unlocked with keycard for " + requiredDivisionType);
                return;
            }
        }

        audioSource.PlayOneShot(accessDenied);
        Debug.Log("Access denied. A valid keycard or override keycard is required.");
        
    }
}
