using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Library added speicfically for this script.

public class Menus : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
    public void ExitApplication()
    {
        // This only works on exported builds, not in the editor's Play Mode.
        Application.Quit();
    }
}
