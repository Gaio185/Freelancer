using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource
    public Slider volumeSlider;     // Reference to the Slider

    void Start()
    {
        // Load the saved volume level, or default to 1
        float savedVolume = PlayerPrefs.GetFloat("AudioVolume", 1f);
        volumeSlider.value = savedVolume; // Set the slider to saved value
        SetVolume(savedVolume);          // Apply saved volume
    }

    public void SetVolume(float volume)
    {
        // Update the AudioSource's volume
        audioSource.volume = volume;

        // Save the volume level
        PlayerPrefs.SetFloat("AudioVolume", volume);
    }
}
