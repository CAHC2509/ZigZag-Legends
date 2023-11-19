using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Space, Header("Unlockable cars settings")]
    [SerializeField]
    private GameObject buyPopUp;
    [SerializeField]
    private TextMeshProUGUI popUpText;
    [SerializeField]
    private GameObject selectCarButton;
    [SerializeField]
    private GameObject buyCarButton;
    [SerializeField]
    private GameObject revealCarButton;
    [SerializeField]
    private GameObject lockImage;
    [SerializeField]
    private List<UnlockableCar> carsUnlockableScripts;

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

        LoadCarsData();
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

            GameObject nextCar = menuCars[currentCarIndex];
            nextCar.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Reset rotatiion
            nextCar.SetActive(true);

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

            GameObject previousCar = menuCars[currentCarIndex];
            previousCar.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Reset rotatiion
            previousCar.SetActive(true);

            UpdateButtonsVisibility();
        }
    }

    /// <summary>
    /// Update the visibility of navigation buttons based on the current car index.
    /// </summary>
    private void UpdateButtonsVisibility()
    {
        // Left and right buttons
        leftButton.SetActive(currentCarIndex > 0);
        rightButton.SetActive(currentCarIndex < menuCars.Count - 1);

        // Lock, select and buy car buttons
        CarData currentCarData = carsUnlockableScripts[currentCarIndex].GetCarData();
        selectCarButton.SetActive(currentCarData.unlocked);
        buyCarButton.SetActive(!currentCarData.unlocked);
        revealCarButton.SetActive(!currentCarData.revealed);
        lockImage.SetActive(!currentCarData.unlocked);
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
    /// Shows a popup with the car name and price to confirm the purchase
    /// </summary>
    public void ShowPurchasePopUp()
    {
        // Cache car data
        string carName = carsUnlockableScripts[currentCarIndex].transform.name;
        int carPrice = carsUnlockableScripts[currentCarIndex].GetCarData().price;

        // Show popup
        popUpText.text = $"Buy {carName}\nfor {carPrice}?";
        buyPopUp.SetActive(true);

        lockImage.SetActive(false);
    }

    /// <summary>
    /// Reveals the current car apparience
    /// </summary>
    public void RevealCar()
    {
        UnlockableCar currentCar = carsUnlockableScripts[currentCarIndex];
        currentCar.RevealCar();

        revealCarButton.SetActive(false);
    }

    /// <summary>
    /// Request the current car purchase
    /// </summary>
    public void PurchaseCar()
    {
        UnlockableCar currentCar = carsUnlockableScripts[currentCarIndex];
        SingleInstanceManager.Managers.carUnlockManager.RequestCarPurchase(currentCar.GetCarData(), currentCar);
    }

    /// <summary>
    /// Load the data from all the unlockable cars
    /// </summary>
    private void LoadCarsData()
    {
        foreach (UnlockableCar car in carsUnlockableScripts)
            car.LoadCarData();
    }

    /// <summary>
    /// Execute the corresponding events when the player selects a car
    /// </summary>
    public void SelectCar() => onCarSelected.Invoke();
}
