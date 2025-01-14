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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip taseSound;

    public GameObject stunBatonUI;

    public CoolDownManager cooldownManager;

    // New field to receive the effect when the baton is equipped
    public GameObject effect;  // This is the new field for the effect

    void Start()
    {
        audioSource.Stop();  // Ensure the sound is stopped when the game starts
        crosshair.SetActive(true);  // Show crosshair by default when the weapon is equipped
    }

    void Update()
    {
        // Check if Fire1 (left mouse button by default) is pressed and use stun baton if conditions are met
        if (Input.GetButton("Fire1") && cooldownManager.readyToUseStunBaton)
        {
            UseStunBaton();
            cooldownManager.readyToUseStunBaton = false;
            cooldownManager.stunBatonTimer = cooldownManager.stunBatonCooldown;
            cooldownManager.stunBatonSlider.value = 0;
        }
    }

    void UseStunBaton()
    {
        // Play the sound instantly when the player clicks, without any cooldown
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = taseSound;
            audioSource.Play();
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

    private void OnEnable()
    {
        stunBatonUI.SetActive(true);

        // Just to show the effect when the stun baton is equipped
        if (effect != null)
        {
            effect.SetActive(true);  // Display the effect
        }

        audioSource.Stop();
    }

    void OnDisable()
    {
        stunBatonUI.SetActive(false);

        // Hide the effect when the stun baton is disabled
        if (effect != null)
        {
            effect.SetActive(false);  // Hide the effect
        }
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
