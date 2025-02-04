using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DivisionType
{
    Worker,
    Executive,
    IT,
    CEO,
    Security
}

public class Keycard : MonoBehaviour
{
    public DivisionType divisionType; // Set this in the Unity Editor to DivisionA, DivisionB, or DivisionC
    private bool isInRange;
    public GameObject interactPanel;
    private Player player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUp;

    public DivisionType GetDivisionType()
    {
        return divisionType;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isInRange = false;
    }

    private void Update() { 
        if(isInRange)
        {
            interactPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F)){
                audioSource.clip = pickUp;
                audioSource.Play();
                player.GetComponent<Player>().keycards.Add(this);
                interactPanel.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.interactionText.text = "Press F to Pick Up";
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactPanel.SetActive(false);
            isInRange = false;
        }
    }
}
