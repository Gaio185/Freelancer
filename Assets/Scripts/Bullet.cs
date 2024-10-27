using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
   public float autoDestroyTime = 5f;
   public float fireRate = 2.0f;
   public float speed = 10f;
   public Rigidbody rb;

    private const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, autoDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage();
        }
        Disable();
    }

    private void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);    
    }
}
