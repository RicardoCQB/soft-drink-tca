using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health=10;
    public GameObject enemyBullet;
    HUD hud;

    public float invulnerabilityTime = 3f;
    private bool isVulnerable;
    private float vulnerableCounter;

    private void Start()
    {
        isVulnerable = true;
        
    }

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

        if(!isVulnerable)
            vulnerableCounter -= Time.deltaTime;
        else
            isVulnerable = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="EnemyBullet" && vulnerableCounter <=0)
        {
            health--;
            hud.UpdateLifeDisplay();
            isVulnerable = false;
            vulnerableCounter = invulnerabilityTime;

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
