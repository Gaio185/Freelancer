using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinLauncher : MonoBehaviour
{
    public Transform cam;
    public Transform spawnPoint;
    public GameObject coin;
    public GameObject coinPlaceholder;

    public int coinCount;
    public float throwCooldown;

    public KeyCode throwKey;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    public GameObject coinsUI;
    public List<GameObject> coinList;

    void Start()
    {
        readyToThrow = true;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && coinCount > 0)
        {
            ThrowCoin();
        }

        if (coinCount <= 0)
        {
            coinPlaceholder.SetActive(false);
        }
        else
        {
           coinPlaceholder.SetActive(true);
        }
    }

    private void ThrowCoin()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(coin, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.forward;
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f)){
            forceDirection = (hit.point - spawnPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + cam.up * throwUpwardForce;

        rb.AddForce(forceToAdd, ForceMode.Impulse);

        --coinCount;

        coinList[coinCount].SetActive(false);

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    public void PickUpCoinUI()
    {
        if(coinCount < coinList.Count)
        {
            coinList[coinCount].SetActive(true);
            coinCount++;
        }
        
    }

    private void ResetThrow()
    {
       readyToThrow = true;
    }

    private void OnEnable()
    {
        coinsUI.SetActive(true);
    }

    private void OnDisable()
    {
        coinsUI.SetActive(false);
    }
}
