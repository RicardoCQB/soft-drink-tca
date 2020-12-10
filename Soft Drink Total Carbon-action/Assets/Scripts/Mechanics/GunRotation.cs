using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;
    public Rigidbody2D player_rb;
    public Rigidbody2D rb;

    public float xOffset = 0.5f;
    public float yOffset = 0.3f;

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 Reference = player_rb.position;



        rb.position = new Vector2(player_rb.position.x - xOffset, player_rb.position.y - yOffset);

    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;

    }
}
