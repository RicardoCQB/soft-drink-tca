using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed=5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
   
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    /*private void FixedUpdate()
    {
        

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }*/
}
