using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Bullet bullet;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = bullet.fireRate;
        bullet = bulletPrefab.GetComponent<Bullet>();
    }

    public void Shoot()
    {
        timer -= Time.deltaTime;

        if (bullet.fireRate <= 0)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.AddForce(bulletSpawn.forward * bullet.speed, ForceMode.Impulse);
            timer = bullet.fireRate;
        }
    }
}
