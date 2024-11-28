using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSound : MonoBehaviour
{
    public AudioSource source;
    public float range = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            source.Play();
            Sounds.MakeSound(new Sound(transform.position, 10f), LayerMask.GetMask("Enemy"), transform);
        }
    }
}
