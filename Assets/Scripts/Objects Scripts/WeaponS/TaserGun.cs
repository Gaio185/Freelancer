using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TaserGun : MonoBehaviour
{
    public float range = 200f;
    public float impactForce = 30f;

    public Camera fpscamera;
    public GameObject crosshair;  // Reference to the crosshair object
    public GameObject taserSoundObject;  // Reference to the GameObject with the audio source (Taser sound GameObject)

    private AudioSource taserAudioSource;  // Reference to the AudioSource component

    public GameObject stunGunUI;

    public CoolDownManager cooldownManager;

    void Start()
    {
        taserAudioSource = taserSoundObject.GetComponent<AudioSource>();  // Get the AudioSource from the GameObject
        taserAudioSource.Stop();  // Ensure the sound is stopped when the game starts
        crosshair.SetActive(true);  // Show crosshair by default when the weapon is equipped
    }

    void Update()
    {
        // Check if Fire1 (left mouse button by default) is pressed and shoot if conditions are met
        if (Input.GetButton("Fire1") && cooldownManager.readyToUseTaser)
        {
            Shoot();
            cooldownManager.readyToUseTaser = false;
            cooldownManager.taserTimer = cooldownManager.taserCooldown;
            cooldownManager.taserSlider.value = 0;
        }

    }

    void Shoot()
    {
        // Play the sound instantly when the player clicks, without any cooldown
        if (!taserAudioSource.isPlaying)
        {
            taserAudioSource.Play();
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
        stunGunUI.SetActive(true);
    }

    void OnDisable()
    {
        stunGunUI.SetActive(false);
        taserAudioSource.Stop();  // Stop sound when weapon is disabled (e.g., switched away)
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
