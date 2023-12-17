using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay events settings")]
    [Space, SerializeField]
    private UnityEvent onPlayerStartMoving;
    [Space, SerializeField]
    private UnityEvent onPlayerDeath;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        SingleInstanceManager.Managers.gameManager = this;
    }

    /// <summary>
    /// Destroy the current car selected for the player
    /// </summary>
    public void DestroyCurrentPlayer() => Destroy(SingleInstanceManager.Player.playerController.gameObject);

    /// <summary>
    /// Executes some custom events (called when the player start moving)
    /// </summary>
    public void PlayerStartMoving() => onPlayerStartMoving?.Invoke();

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

    /// <summary>
    /// Close the game
    /// </summary>
    public void CloseGame() => Application.Quit();
}
