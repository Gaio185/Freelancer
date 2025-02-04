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

    [SerializeField] private AudioSource audioSource;  // Reference to the AudioSource component
    [SerializeField] private AudioClip taseSound;

    public ParticleSystem lightningParticles; // Particle system for the lightning effect
    public GameObject shaderGraphLightningEffectPrefab; // Prefab for the Shader Graph-based VFX

    public GameObject stunGunUI;

    public CoolDownManager cooldownManager;
    public Transform fireDirection;

    void Start()
    {
        audioSource.Stop();  // Ensure the sound is stopped when the game starts
        
        crosshair.SetActive(true);  // Show crosshair by default when the weapon is equipped

        // Deactivate particle system initially
        if (lightningParticles != null)
        {
            lightningParticles.Stop();  // Stop particle system
        }

        // Ensure shader effect is deactivated initially
        if (shaderGraphLightningEffectPrefab != null)
        {
            shaderGraphLightningEffectPrefab.SetActive(false);  // Ensure shader effect is inactive initially
        }
    }

    void Update()
    {
        // Check if Fire1 (left mouse button by default) is pressed and shoot if conditions are met
        if (Input.GetMouseButton(0) && cooldownManager.readyToUseTaser)
        {
            Shoot();
            cooldownManager.readyToUseTaser = false;
            cooldownManager.taserTimer = cooldownManager.taserCooldown;
            cooldownManager.taserSlider.value = 0;
        }
    }

    void Shoot()
    {
        // Play Taser sound if not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = taseSound;
            audioSource.Play();
        }

        // Play the particle effect (lightning)
        if (lightningParticles != null)
        {
            lightningParticles.Play();  // Play the particle system immediately
        }

        // Instantiate and play the shader effect when shooting
        if (shaderGraphLightningEffectPrefab != null && fireDirection != null)
        {
            // Instantiate the shader effect at the fire direction's position and rotation
            GameObject shaderEffect = Instantiate(shaderGraphLightningEffectPrefab, fireDirection.position, fireDirection.rotation);

            // Ensure the effect is activated at the moment of shooting
            shaderEffect.SetActive(true);  // Activate the effect

            // Destroy the shader effect after 2 seconds to prevent clutter
            Destroy(shaderEffect, 2f);
        }

        // Raycast logic and impact effects
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
        audioSource.Stop();
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
