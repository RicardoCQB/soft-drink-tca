using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private HUD hud;

    public float bulletForce = 10f;

    private void Start()
    {
        hud = gameObject.GetComponent<HUD>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            hud.ReduceWeaponMag();
            hud.UpdateWeaponDisplay();
        }
        
    }

    void Shoot()
    {
        //instantiates the bullet prefab according to the fire point position/rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position,
                                                            firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

        
    }
}
