using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;
    public Transform rightGunPos;
    public Transform leftGunPos;
    private SpriteRenderer gunSprite;

    private void Start()
    {
        gunSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);

        if (angle > 0 && angle <= 180)
        {
            this.gameObject.transform.position = rightGunPos.position;
            gunSprite.sortingLayerName = "GunBehindPlayer";
        }
        else
        {
            this.gameObject.transform.position = leftGunPos.position;
            gunSprite.sortingLayerName = "Gun";
        }
    }
}
