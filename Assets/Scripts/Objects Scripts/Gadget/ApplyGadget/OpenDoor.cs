using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Door;

public class OpenDoor : MonoBehaviour
{
    private Player player;
    private bool isInRange;
    public Door door;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.F)) && door.isUnlocked && isInRange)  // Use mouse click for interaction
        {
            door.TryOpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
            player.interactionText.text = "Press F to Open Door";
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
