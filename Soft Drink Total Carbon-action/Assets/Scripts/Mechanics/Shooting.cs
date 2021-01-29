using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public HUD hud;

    public float bulletForce = 10f;
    public bool bulletsAvailable;
    bool playerIsBusy = false;

    private void Start()
    {
        bulletsAvailable = true;
    }

    void Update()
    {
        if (bulletsAvailable == true && playerIsBusy == false)
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
    public void MakePlayerBusy()
    {
        // This stops the shooting from happening, if the player is in the shop or pause menu
        playerIsBusy = true;
    }
    public void MakePlayerFree()
    {
        // This allows the shooting again.
        playerIsBusy = false;
    }
}
