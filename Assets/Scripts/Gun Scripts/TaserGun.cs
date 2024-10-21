using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TaserGun : MonoBehaviour
{
    public float range = 200f;
    public float impactForce = 30f;
    public float cooldown = 10.0f;

    public Camera fpscamera;
    //public ParticleSystem muzzleflash;
    //public GameObject impactEffect;

    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && timer <= 0)
        {
            timer = cooldown;
            Shoot();
        }

    }

    void Shoot()
    {
        //muzzleflash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            AiAgent target = hit.transform.GetComponent<AiAgent>();
            if (target != null)
            {
                target.stateMachine.ChangeState(AiStateId.Stunned);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 2f);
        }

    }

}
