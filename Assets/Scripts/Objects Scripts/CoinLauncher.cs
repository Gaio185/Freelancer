using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(coinCount <= 0)
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

        coinCount--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
       readyToThrow = true;
    }
}
