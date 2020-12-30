using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health=10;
    public GameObject enemyBullet;
    HUD hud;

    private void Start()
    {
        hud = gameObject.GetComponent<HUD>();
    }
    private void Update()
    {
        if(health <=0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="EnemyBullet")
        {
            hud.ReduceLifeDisplay(20);
            health--;
        }    
    }

}
