using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cars;
    [SerializeField]
    private Transform spawnPoint;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        SingletonManager.Managers.gameManager = this;
    }

    public void SelectCar()
    {
        int randomIndex = Random.Range(0, cars.Count);
        Instantiate(cars[randomIndex], spawnPoint.position, spawnPoint.rotation, null);
        
        SingletonManager.Player.cameraFollow.FindPlayer();

        List<PlatformController> platformControllers = SingletonManager.WorldObjects.instanciatedPlatformControllers;
        foreach (PlatformController platformController in platformControllers)
            platformController.FindPlayer();
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
