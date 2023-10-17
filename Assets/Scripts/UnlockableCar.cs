using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableCar : MonoBehaviour
{
    [SerializeField]
    private CarData carData;

    private void Start() => carData.unlocked = PlayerPrefsUtility.GetCarUnlockedState(carData.name);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>CarData from current car</returns>
    public CarData GetCarData() => carData;

    /// <summary>
    /// Change the current CarData unlocked state to true and saves it on PlayerPrefs
    /// </summary>
    public void UnlockCar()
    {
        carData.unlocked = true;
        PlayerPrefsUtility.SetCarUnlockedState(carData.name, 1);
    }
}
