using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float autoDestroyTime = 1f;
    public float speed = 10f;
    public Rigidbody rb;
    public ParticleSystem topExplosion;
    public ParticleSystem bottomExplosion;
    public GameObject innerBullet;

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
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TookDamage");
            player.GetComponent<PlayerHealth>().TakeDamage();
            DestroyBullet();
        }
        else if(((1 << other.gameObject.layer) & LayerMask.GetMask("Wall")) != 0)
        {
            DestroyBullet();
        }
    }

    private void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void DestroyBullet()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = false;
        if (innerBullet != null)
        {
            MeshRenderer innerMr = innerBullet.GetComponent<MeshRenderer>();
            innerMr.enabled = false;
        }
        rb.AddForce(this.gameObject.transform.forward * -speed, ForceMode.VelocityChange);
        topExplosion.Play();
        bottomExplosion.Play();
        if (topExplosion != null && bottomExplosion != null && !topExplosion.isPlaying && !bottomExplosion)
        {
            Destroy(this.gameObject);
        }
    }
}
