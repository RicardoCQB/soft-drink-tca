using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;


    private Transform player;
    private Vector2 target;
    private Vector2 overshoot; //if we want it to keep going after it would stop

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        //player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        //makes the projectile keep going instead of stopping at its destination (thats how "Vector2.MoveTowards() works)
        overshoot = (player.position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;

        Vector3 overshotPos = new Vector3(target.x + overshoot.x, target.y + overshoot.y, 1);

        //shoot bullet towards player
        transform.position = Vector2.MoveTowards(transform.position, overshotPos , speed * Time.deltaTime);

        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);

        //if the projectile stops (if its overshooting, this probably wont be necessary)
        if (currentPosition == transformPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
