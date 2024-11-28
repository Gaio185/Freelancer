using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerDetect : MonoBehaviour
{
    public float radius = 3f;
    public LayerMask targetMask;
    public GameObject weaponHolder;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
                weaponHolder.SetActive(true);
            }
        }
    }
}
