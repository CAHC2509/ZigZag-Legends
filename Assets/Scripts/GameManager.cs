using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay events settings")]
    [Space, SerializeField]
    private UnityEvent onPlayerDeath;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        SingletonManager.Managers.gameManager = this;
    }

    /// <summary>
    /// Executes some custom events (called when the player falls)
    /// </summary>
    public void PlayerDeath() => onPlayerDeath.Invoke();

    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause() => Time.timeScale = 0;

    /// <summary>
    /// Unpause the game
    /// </summary>
    public void Continue() => Time.timeScale = 1;

    /// <summary>
    /// Restart the current scene
    /// </summary>
    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
}
