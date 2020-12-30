using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public Transform firePoint;
    public GameObject projectile;
    public Transform player;

    bool playerAlive = true;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
    }

    private void Update()
    {
        playerAlive=GameObject.FindGameObjectWithTag("Player");

        if(playerAlive)
        { 
            //ENEMY MOVEMENT
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position,
                                                                   speed * Time.deltaTime);

            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance
                        && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position,
                                                                   -speed * Time.deltaTime);
            }

            //ENEMY SHOOTING
            if (timeBetweenShots <= 0)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }

            }

    }
}
