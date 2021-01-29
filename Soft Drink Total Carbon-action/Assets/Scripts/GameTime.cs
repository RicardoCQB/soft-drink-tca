using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    float defaultGameTimeScale;
    private void Start()
    {
        defaultGameTimeScale = Time.timeScale;
    }
    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void RestartTime()
    {
        Time.timeScale = defaultGameTimeScale;
    }
}
