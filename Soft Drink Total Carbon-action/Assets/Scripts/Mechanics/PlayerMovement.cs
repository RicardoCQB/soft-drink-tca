using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Library added to this script

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed=5f;

    public Rigidbody2D rb;
    public Camera cam;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;

    public UnityEvent openStore;

    private string currentState;
    private bool isWalking;
    AudioSource steps;
    // Player animation states
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

    Vector2 movement;
    Vector2 mousePos;


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.Play(FRONT_IDLE);
        steps = GetComponent<AudioSource>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
            isWalking = true;
        else
            isWalking = false;

        // Moves the player in the level
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (isWalking)
        {
            if (!steps.isPlaying)
            {
                steps.Play();
            }
        }
        else
            steps.Stop();



        
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //Debug.Log("\nAngle: " + angle);

        if (isWalking)
        {
            if (angle > -65f && angle <= 0f)
                ChangeAnimationState(RIGHT_FRONT_WALKING);
            else if (angle > 0f && angle <= 65f)
                ChangeAnimationState(RIGHT_BACK_WALKING);
            else if (angle > 65f && angle <= 115f)
                ChangeAnimationState(BACK_WALKING);
            else if (angle > 115f && angle <= 180f)
                ChangeAnimationState(LEFT_BACK_WALKING);
            else if (angle > -180f && angle <= -115f)
                ChangeAnimationState(LEFT_FRONT_WALKING);
            else if (angle > -115f && angle <= -65f)
                ChangeAnimationState(FRONT_WALKING);
        }

        if (!isWalking)
        {
            if (angle > -65f && angle <= 0f)
                ChangeAnimationState(FRONT_RIGHT_IDLE);
            else if (angle > 0f && angle <= 65f)
                ChangeAnimationState(BACK_RIGHT_IDLE);
            else if (angle > 65f && angle <= 115f)
                ChangeAnimationState(BACK_IDLE);
            else if (angle > 115f && angle <= 180f)
                ChangeAnimationState(BACK_LEFT_IDLE);
            else if (angle > -180f && angle <= -115f)
                ChangeAnimationState(FRONT_LEFT_IDLE);
            else if (angle > -115f && angle <= -65f)
                ChangeAnimationState(FRONT_IDLE);
        }

    }

    void ChangeAnimationState(string newState)
    {
        // If the current state is already playing, the function does nothing.
        if (currentState == newState) return;

        // Plays the animation state passed as a parameter.
        playerAnimator.Play(newState);

        // Updates the current state animation.
        currentState = newState;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // In "GetMouseButtonDown", an int value of 0 is the left button and 1 is the right button.
        if (collision.tag == "Shopkeeper" && Input.GetMouseButtonDown(0))
        {
            openStore.Invoke();
        }
    }
}
