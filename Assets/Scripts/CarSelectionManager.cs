using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [Space, SerializeField]
    private UnityEvent onCarSelected;

    private int currentCarIndex = 0; // Index of the currently car displayed

    private void Start()
    {
        currentCarIndex = PlayerPrefsUtility.GetCarSelectedIndex();
        ShowLastSelectedCar();
        UpdateButtonsVisibility();
    }

    private void ShowLastSelectedCar() => menuCars[currentCarIndex].SetActive(true);

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
    /// Spawn the actual car showed in the menu
    /// </summary>
    public void SpawnCarSelected()
    {
        PlayerPrefsUtility.SetCarSelectedIndex(currentCarIndex);
        Instantiate(inGameCars[currentCarIndex], spawnPoint.position, spawnPoint.rotation, null);
    }

    /// <summary>
    /// Execute the corresponding events when the player selects a car
    /// </summary>
    public void SelectCar() => onCarSelected.Invoke();
}
