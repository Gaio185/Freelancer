using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Bullet bullet;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bullet = bulletPrefab.GetComponent<Bullet>();
    }

    public void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (bulletSpeed > 0) bulletRig.AddForce(this.gameObject.transform.forward * bullet.speed, 
                                                ForceMode.VelocityChange);
        bulletObj.GetComponent<Bullet>().PlayAmbientAudio();
        Destroy(bulletObj, bullet.autoDestroyTime);
    }
}
