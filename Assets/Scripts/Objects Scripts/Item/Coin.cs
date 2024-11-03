using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float pickUpRange;
    public LayerMask targetMask;

    private GameObject launchPoint;
    private CoinLauncher coinLauncher;

    void Start()
    {
        launchPoint = GameObject.FindGameObjectWithTag("CoinSpawn");
        coinLauncher = launchPoint.GetComponent<CoinLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, pickUpRange, targetMask);

        if (rangeChecks.Length != 0 && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
            coinLauncher.coinCount++;
        }
    }
}
