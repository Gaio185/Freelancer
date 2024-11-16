using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSkipper : MonoBehaviour
{
    public GameObject[] images; // Array of GameObjects to cycle through
    private int currentIndex = 0; // Tracks the current active image

    void Start()
    {
        // Ensure only the first image is active at the start
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i == 0); // Activate the first image, deactivate the rest
        }
    }

    public void SkipImage()
    {
        if (images.Length == 0) return; // Exit if no images are assigned

        // Deactivate the current image
        images[currentIndex].SetActive(false);

        // Move to the next image, looping back if needed
        currentIndex = (currentIndex + 1) % images.Length;

        // Activate the next image
        images[currentIndex].SetActive(true);
    }
}
