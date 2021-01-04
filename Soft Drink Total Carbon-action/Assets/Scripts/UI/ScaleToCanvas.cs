using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToCanvas : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    RectTransform thisObject;
    int prevScreenWidth, prevScreenHeight;

    private void Awake()
    {
        thisObject = this.GetComponent<RectTransform>();
    }
    private void Start()
    {
            prevScreenWidth = Screen.width;
            prevScreenHeight = Screen.height;
            thisObject.sizeDelta = canvas.sizeDelta;
    }
}
