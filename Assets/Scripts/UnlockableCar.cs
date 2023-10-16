using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableCar : MonoBehaviour
{
    [SerializeField]
    private CarData carData;

    private void Start() => carData.unlocked = PlayerPrefsUtility.GetCarUnlockedState(carData.name);

    public CarData GetCarData() => carData;
}
