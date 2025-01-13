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

    private Player player;

    void Start()
    {
        launchPoint = GameObject.FindGameObjectWithTag("CoinSpawn");
        coinLauncher = launchPoint.GetComponent<CoinLauncher>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, pickUpRange, targetMask);

        if (rangeChecks.Length != 0)
        {
            player.interactionText.text = "Press F to Pick Up";
            player.interactPanel.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F) && coinLauncher.coinCount < coinLauncher.coinList.Count)
            {
                Destroy(this.gameObject);
                player.interactPanel.SetActive(false);
                coinLauncher.PickUpCoinUI();
            }
        }
        else
        {
            player.interactPanel.SetActive(false);
        }
    }
}
