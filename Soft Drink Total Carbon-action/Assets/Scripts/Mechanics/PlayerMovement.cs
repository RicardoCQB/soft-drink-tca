using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed=5f;

    public Rigidbody2D rb;
    public Camera cam;
    public SpriteRenderer playerSprite;

    // These booleans are responsible for detecting which sprite and animation to use for the character movement.
    public bool isDirUp, isDirDown, isDirRight, isDirLeft = false;
    public bool isDirUpRight, isDirUpLeft, isDirDownRight, isDirDownLeft = false;

    Vector2 movement;
    Vector2 mousePos;
   
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Moves the player in the level
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //Debug.Log("\nAngle: " + angle);

        if (angle > -25f && angle <= 25f)
            isDirLeft = true;
        else if (angle > 25f && angle <= 65f)
            isDirUpLeft = true;
        else if (angle > 65f && angle <= 115f)
            isDirUp = true;
        else if (angle > 115f && angle <= 155f)
            isDirUpRight = true;
        else if ((angle > 155f && angle <= 180f) || (angle > -180f && angle <= -155f))
            isDirRight = true;
        else if (angle > -155f && angle <= -115f)
            isDirDownRight = true;
        else if (angle > -115f && angle <= -65f)
            isDirDown = true;
        else if (angle > -65f && angle <= -25f)
            isDirDownLeft = true;
    }
}
