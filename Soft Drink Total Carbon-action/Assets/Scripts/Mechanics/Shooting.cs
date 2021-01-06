using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private HUD hud;

    public float bulletForce = 10f;
    public bool bulletsAvailable;

    private void Start()
    {
        hud = gameObject.GetComponent<HUD>();
        bulletsAvailable = true;
    }

    void Update()
    {
        if (bulletsAvailable == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                hud.ReduceWeaponMag();
                hud.UpdateWeaponDisplay();
            }
        }
        
    }

    void Shoot()
    {
        //instantiates the bullet prefab according to the fire point position/rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position,
                                                            firePoint.rotation);

        FindObjectOfType<AudioManager>().Play("BulletPop");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

        
    }

    public void StopShooting()
    {
        bulletsAvailable = false;
    }

    public void StartShooting()
    {
        bulletsAvailable = true;
    }
}
