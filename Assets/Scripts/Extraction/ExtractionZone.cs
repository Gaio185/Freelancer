using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionZone : MonoBehaviour
{
    [HideInInspector] public bool isInRange;

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
            isInRange = false;
        }
    }
}
