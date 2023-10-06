using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause() => Time.timeScale = 0;

    /// <summary>
    /// Unpause the game
    /// </summary>
    public void Continue() => Time.timeScale = 1;
}
