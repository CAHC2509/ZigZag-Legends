using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionManager : MonoBehaviour
{
    [Header("Menu settings")]
    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject rightButton;
    [SerializeField]
    private List<GameObject> menuCars;

    [Space, Header("Car spawn settings")]
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private List<GameObject> inGameCars;

    private int currentCarIndex = 0; // Index of the currently car displayed

    private void Start() => UpdateButtonsVisibility();

    /// <summary>
    /// Display the next car in the sequence if available.
    /// </summary>
    public void NextCar()
    {
        if (currentCarIndex < menuCars.Count - 1)
        {
            menuCars[currentCarIndex].SetActive(false);
            currentCarIndex++;
            menuCars[currentCarIndex].SetActive(true);

            UpdateButtonsVisibility();
        }
    }

    /// <summary>
    /// Display the previous car in the sequence if available.
    /// </summary>
    public void PreviousCar()
    {
        if (currentCarIndex > 0)
        {
            menuCars[currentCarIndex].SetActive(false);
            currentCarIndex--;
            menuCars[currentCarIndex].SetActive(true);

            UpdateButtonsVisibility();
        }
    }

    /// <summary>
    /// Update the visibility of navigation buttons based on the current car index.
    /// </summary>
    private void UpdateButtonsVisibility()
    {
        leftButton.SetActive(currentCarIndex > 0);
        rightButton.SetActive(currentCarIndex < menuCars.Count - 1);
    }

    /// <summary>
    /// Select and spawn the actual car showed in the menu
    /// </summary>
    public void SelectCar()
    {
        Instantiate(inGameCars[currentCarIndex], spawnPoint.position, spawnPoint.rotation, null);

        SingletonManager.Player.cameraFollow.FindPlayer();

        List<PlatformController> platformControllers = SingletonManager.WorldObjects.instanciatedPlatformControllers;
        foreach (PlatformController platformController in platformControllers)
            platformController.FindPlayer();
    }
}
