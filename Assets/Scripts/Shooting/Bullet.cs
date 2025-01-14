using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float autoDestroyTime = 1f;
    public float speed;
    public Rigidbody rb;
    public ParticleSystem topExplosion;
    public ParticleSystem bottomExplosion;
    public GameObject trail;
    public GameObject innerBullet;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ambientSound;
    [SerializeField] private AudioClip explosionSound;

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

    public void PlayAmbientAudio()
    {
        audioSource.volume = 1.0f;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(ambientSound);
    }

    private void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void DestroyBullet()
    {
        Destroy(trail);
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
        audioSource.Stop();
        audioSource.volume = 0.3f;
        audioSource.pitch = 0.8f;
        audioSource.PlayOneShot(explosionSound);
        if (topExplosion != null && bottomExplosion != null && !topExplosion.isPlaying && !bottomExplosion)
        {
            Destroy(this.gameObject);
        }
    }
}
