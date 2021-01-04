using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Library added speicfically for this script.

public class Menus : MonoBehaviour
{
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
