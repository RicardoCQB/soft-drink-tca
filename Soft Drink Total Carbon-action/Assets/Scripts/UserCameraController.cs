using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCameraController : MonoBehaviour
{
    public Transform Player;
    public Camera cam;
    public float dampTime = 0.4f;
    private Vector2 mousePos;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        //TODO: Try out if statement that calculates distance from player to mouse and decides if the camera is moved a bit to the mouse transform.

        if (Player != null)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            //cameraPos = new Vector3((Player.position.x + mousePos.x)/2, (Player.position.y + mousePos.y)/2, -10f);
            cameraPos = new Vector3(Player.position.x, Player.position.y, -10f);
        }
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, cameraPos, ref velocity, dampTime);
    }
}