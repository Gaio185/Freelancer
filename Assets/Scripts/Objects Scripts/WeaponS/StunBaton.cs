using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBaton : MonoBehaviour
{
    public float range = 30f;
    public float impactForce = 30f;

    public Camera fpscamera;
    public GameObject crosshair;  // Reference to the crosshair object
    public GameObject stunSoundObject;  // Reference to the GameObject with the audio source (Stun sound GameObject)

    private AudioSource stunAudioSource;  // Reference to the AudioSource component

    void Start()
    {
        stunAudioSource = stunSoundObject.GetComponent<AudioSource>();  // Get the AudioSource from the GameObject
        stunAudioSource.Stop();  // Ensure the sound is stopped when the game starts
        crosshair.SetActive(true);  // Show crosshair by default when the weapon is equipped
    }

    void Update()
    {
        // Check if Fire1 (left mouse button by default) is pressed and use stun baton if conditions are met
        if (Input.GetButton("Fire1"))
        {
            UseStunBaton();
        }
    }

    void UseStunBaton()
    {
        // Play the sound instantly when the player clicks, without any cooldown
        if (!stunAudioSource.isPlaying)
        {
            stunAudioSource.Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            AiAgent target = hit.transform.GetComponent<AiAgent>();
            if (target != null)
            {
                target.stateMachine.ChangeState(AiStateId.Stunned);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    void OnDisable()
    {
        stunAudioSource.Stop();  // Stop sound when weapon is disabled (e.g., switched away)
    }

    // To hide crosshair when switching to non-weapon items
    public void HideCrosshair()
    {
        crosshair.SetActive(false);  // Hide the crosshair
    }

    // To show crosshair when switching back to weapon
    public void ShowCrosshair()
    {
        crosshair.SetActive(true);  // Show the crosshair
    }
}
