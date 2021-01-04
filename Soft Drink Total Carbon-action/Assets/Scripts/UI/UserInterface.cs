using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Library added specifically for this script.

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
    public void LoadScene(string sceneToLoad)
    {
        // This will replace the active scene with the one you want to load.
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
    public void ExitApplication()
    {
        // This only works on exported builds, not in the editor's Play Mode.
        Application.Quit();
    }
}
