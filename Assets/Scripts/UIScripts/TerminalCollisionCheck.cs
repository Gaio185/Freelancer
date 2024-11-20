using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalCollisionCheck : MonoBehaviour
{
    [HideInInspector] public bool isInRange;
    public GameObject interactPanel; // Reference to the interact panel

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactPanel.SetActive(false); // Hide the interact panel
            isInRange = false;
        }
    }
}
