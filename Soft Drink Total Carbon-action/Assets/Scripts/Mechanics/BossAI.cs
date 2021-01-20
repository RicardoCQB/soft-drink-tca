using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float activationDistance;
    public int health = 10;

    private float timeBetweenShots;
    public float startTimeBetweenShots;
    private float timeBetweenBubbles;
    public float startTimeBetweenBubbles;

    private Transform fizz;
    
    public GameObject projectile;
    public Transform player;
    public Rigidbody2D playerRB;
    public GameObject bubbleParticles;
    public GameObject particleRotator;
    public GameObject lastCola;

    public Transform firePoint;
    public Transform firePoint_L;
    public Transform firePoint_R;
    public Transform firePoint_D;
    public Transform firePoint_U;
    //public Animator enemyAnimator;


    bool playerAlive;

    private string currentState;
    private bool isWalking;

    // These constants are for handling the enemy animation.
    const string FRONT_IDLE = "Front_Idle";
    const string BACK_IDLE = "Back_Idle";
    const string FRONT_LEFT_IDLE = "FrontLeft_Idle";
    const string FRONT_RIGHT_IDLE = "FrontRight_Idle";
    const string BACK_LEFT_IDLE = "BackLeft_Idle";
    const string BACK_RIGHT_IDLE = "BackRight_Idle";

    const string FRONT_WALKING = "Front_Walking";
    const string BACK_WALKING = "Back_Walking";
    const string LEFT_FRONT_WALKING = "LeftFront_Walking";
    const string RIGHT_FRONT_WALKING = "RightFront_Walking";
    const string LEFT_BACK_WALKING = "LeftBack_Walking";
    const string RIGHT_BACK_WALKING = "RightBack_Walking";

    private void Start()
    {
        isWalking = false;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
        timeBetweenBubbles = startTimeBetweenBubbles;

        //enemyAnimator = GetComponent<Animator>();
        //enemyAnimator.Play(FRONT_WALKING);

        playerAlive = true;
    }

    private void Update()
    {
        //HEALTH MANAGMENT
        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            lastCola.SetActive(true);
            Destroy(gameObject);
        }


        playerAlive = GameObject.FindGameObjectWithTag("Player");


        if (playerAlive)
        {
            if (Vector2.Distance(transform.position, player.position) < activationDistance)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            if (isWalking)
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
                    FindObjectOfType<AudioManager>().Play("BUBBLE");
                    timeBetweenShots = startTimeBetweenShots;
                }
                else
                {
                    timeBetweenShots -= Time.deltaTime;
                }

                Vector2 lookDir = playerRB.position - gameObject.GetComponent<Rigidbody2D>().position;

                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

                //particleRotator.transform.rotation = Quaternion.LookRotation(player.position) * Quaternion.FromToRotation(Vector3.right, Vector3.forward); 

                if (timeBetweenBubbles<=0)
                {
                    if (angle > 0f && angle <= 90f)
                    {
                        Instantiate(bubbleParticles, firePoint_R.position, gameObject.transform.rotation);
                        FindObjectOfType<AudioManager>().Play("FizzJet");
                        timeBetweenBubbles = startTimeBetweenBubbles;
                    }
                    else if (angle > 90f && angle <= 180f)
                    {
                        Instantiate(bubbleParticles, firePoint_L.position, gameObject.transform.rotation);
                        FindObjectOfType<AudioManager>().Play("FizzJet");
                        timeBetweenBubbles = startTimeBetweenBubbles;
                    }
                    else if (angle > -180f && angle <= -90f)
                    {
                        Instantiate(bubbleParticles, firePoint_D.position, gameObject.transform.rotation);
                        FindObjectOfType<AudioManager>().Play("FizzJet");
                        timeBetweenBubbles = startTimeBetweenBubbles;
                    }
                    else if (angle > -90f && angle <= 0)
                    {
                        Instantiate(bubbleParticles, firePoint_L.position, gameObject.transform.rotation);
                        FindObjectOfType<AudioManager>().Play("FizzJet");
                        timeBetweenBubbles = startTimeBetweenBubbles;
                    }
                   
                    
                }
                else
                {
                    timeBetweenBubbles -= Time.deltaTime;
                }

            }
            else
            {
                isWalking = false;
                playerAlive = false;
            }
        }


        //if (isWalking && playerAlive)
        //{
        //    Vector2 lookDir = player.position - transform.position;
        //    //Debug.Log("\npos: " + lookDir);
        //    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //    //Debug.Log("\nAngle: " + angle);

        //    if (angle > -65f && angle <= 0f)
        //        ChangeAnimationState(RIGHT_FRONT_WALKING);
        //    else if (angle > 0f && angle <= 65f)
        //        ChangeAnimationState(RIGHT_BACK_WALKING);
        //    else if (angle > 65f && angle <= 115f)
        //        ChangeAnimationState(BACK_WALKING);
        //    else if (angle > 115f && angle <= 180f)
        //        ChangeAnimationState(LEFT_BACK_WALKING);
        //    else if (angle > -180f && angle <= -115f)
        //        ChangeAnimationState(LEFT_FRONT_WALKING);
        //    else if (angle > -115f && angle <= -65f)
        //        ChangeAnimationState(FRONT_WALKING);
        //}

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }
    }

    //void ChangeAnimationState(string newState)
    //{
    //    // If the current state is already playing, the function does nothing.
    //    if (currentState == newState) return;

    //    // Plays the animation state passed as a parameter.
    //    enemyAnimator.Play(newState);

    //    // Updates the current state animation.
    //    currentState = newState;
    //}
}
