using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    Vector2 cursorTxtOffset;
    private void Start()
    {
        // By default, the pivot of a cursor texture is the upper left corner.
        // This changes the pivot to the center, for better precision:
        cursorTxtOffset = new Vector2(texture.width / 2, texture.height / 2);

        // Changes the cursor texture to the one we want:
        Cursor.SetCursor(texture, cursorTxtOffset, CursorMode.Auto);
    }
}
