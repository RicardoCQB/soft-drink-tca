using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    public Image image1;
    public Sprite[] SpriteArray;
    private int currentImage = 0;
    public float fadeTime = 2f;
    public bool fadefinished = false;

    private float deltaTime = 0.0f;

    public float timer1 = 5.0f;
    private float timer1Remaining = 5.0f;
    public bool timer1IsRunning = true;

    void Start()
    {
        image1.canvasRenderer.SetAlpha(0.0f);
        image1.sprite = SpriteArray[currentImage];

        image1.CrossFadeAlpha(1, fadeTime, false);

        bool timer1IsRunning = false;
        // timer1 should be bigger than fade time 
        timer1Remaining = timer1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            #else
            Application.Quit();
            #endif
        }

        if (timer1IsRunning)
        {
            if (timer1Remaining > 0)
            {
                timer1Remaining -= Time.deltaTime;
                image1.CrossFadeAlpha(1, fadeTime, false);
            }

            else
            {
                UnityEngine.Debug.Log("Timer1 has finished!");
                timer1Remaining = timer1;
                fadefinished = true;
                //image1.sprite = SpriteArray[currentImage];
                timer1IsRunning = !timer1IsRunning;
                image1.CrossFadeAlpha(0, fadeTime, false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            UnityEngine.Debug.Log("Pressed primary button.");
            currentImage++;

            if (currentImage >= SpriteArray.Length)
                SceneManager.LoadScene("MechanicTestScene");


            fade();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Pressed space bar.");
            currentImage++;

            if (currentImage >= SpriteArray.Length)
                SceneManager.LoadScene("MechanicTestScene");

            fade();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UnityEngine.Debug.Log("Pressed RightArrow.");
            currentImage++;

            if (currentImage >= SpriteArray.Length)
                SceneManager.LoadScene("MechanicTestScene");

            fade();
        }

        if (Input.GetMouseButtonDown(1))
        {
            UnityEngine.Debug.Log("Pressed secondary button.");

            currentImage--;
            if (currentImage < 0)
                currentImage = 0;
            fade();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            UnityEngine.Debug.Log("Pressed Backspace.");

            currentImage--;
            if (currentImage < 0)
                currentImage = 0;
            //currentImage = (SpriteArray.Length-1);

            fade();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UnityEngine.Debug.Log("Pressed LeftArrow.");
            currentImage--;

            if (currentImage < 0)
                currentImage = 0;
            //currentImage = (SpriteArray.Length-1);

            fade();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //ispaused = !ispaused;
            timer1IsRunning = !timer1IsRunning;
        }

        void fade()
        {
            image1.canvasRenderer.SetAlpha(0.0f);
            image1.sprite = SpriteArray[currentImage];
            timer1Remaining = timer1;
            timer1IsRunning = true;
        }
    }
}
