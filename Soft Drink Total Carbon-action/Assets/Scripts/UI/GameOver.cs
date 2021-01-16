using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Library added for this script.
using UnityEngine.SceneManagement; // Library added for this script.

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject stevenUp;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float frameTimeInSecs;
    string nameOfThisScene;
    bool playerIsDead = false;

    private void Start()
    {
        gameOverText.SetActive(false);
        nameOfThisScene = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        if (stevenUp == null && playerIsDead == false)
        {
            gameOverText.SetActive(true);
            StartCoroutine("ReloadScene");
            playerIsDead = true;
        }
    }
    IEnumerator ReloadScene()
    {
        // We wait some period before continuing this coroutine.
        yield return new WaitForSeconds(frameTimeInSecs);
        // Switch scene:
        SceneManager.LoadScene(nameOfThisScene, LoadSceneMode.Single);
    }
}
