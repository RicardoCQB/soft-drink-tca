using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    // This script contains elements reused throughout all elements of the UI (such as the menus or the HUD)

    public void EnableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void DisableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
