using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalCollisionCheck : MonoBehaviour
{
    [HideInInspector] public bool isInRange;
    [HideInInspector] public Player player;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.interactionText.text = "Press F to Interact";
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.interactPanel.SetActive(false); // Hide the interact panel
            isInRange = false;
        }
    }
}
