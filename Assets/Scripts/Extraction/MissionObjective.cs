using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionObjective : MonoBehaviour
{
    private Player player;
    private bool isInRange;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            player.interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            player.interactPanel.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isInRange)
        {
            player.movement.canExtract = true;
            ObjectiveManager manager = FindObjectOfType<ObjectiveManager>();
            if (manager != null)
            {
                manager.CompleteObjective();
            }
            player.interactPanel.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
