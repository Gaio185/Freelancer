using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Door;

public class OpenDoor : MonoBehaviour
{
    private Player player;
    private bool isInRange;
    public Door door;  // Reference to the Door script

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && door.isUnlocked && isInRange)  // Use F key for interaction
        {
            door.TryToggleDoor();
        }

        if (!isInRange)
        {
            player.interactPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
            player.interactionText.text = "Press F to Open/Close Door";
            player.interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            player.interactPanel.SetActive(false);
        }
    }
}
