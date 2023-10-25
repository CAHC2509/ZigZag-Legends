using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableCar : MonoBehaviour
{
    [SerializeField]
    private CarData carData;
    [SerializeField]
    private bool isDefaultCar = false;

    public void LoadCarData()
    {
        carData.unlocked = PlayerPrefsUtility.GetCarUnlockedState(carData.name);

        if (isDefaultCar)
            carData.unlocked = true;
    }

    /// <summary>
    /// Change the current CarData unlocked state to true and saves it on PlayerPrefs
    /// </summary>
    public void UnlockCar()
    {
        carData.unlocked = true;
        PlayerPrefsUtility.SetCarUnlockedState(carData.name, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>CarData from current car</returns>
    public CarData GetCarData() => carData;
}
