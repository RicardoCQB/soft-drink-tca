﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health=10;
    public GameObject enemyBullet;
    HUD hud;

    private void Awake()
    {
        hud = gameObject.GetComponent<HUD>();
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="EnemyBullet")
        {
            health--;
            hud.UpdateLifeDisplay();
        }    
    }

    public void IncreaseHealth(int healthIncrement)
    {
        // This function is used in the stop, to allow for the health to be increased
        // when the player buys hearts.

        if (health + healthIncrement >= 5)
        {
            health = 5;
        }
        else health += healthIncrement; 
    }

}
