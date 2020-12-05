using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Camera cam;
    private void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
