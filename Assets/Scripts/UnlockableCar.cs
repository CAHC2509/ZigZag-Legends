using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableCar : MonoBehaviour
{
    [Header("Car data settings")]
    [SerializeField]
    private CarData carData;
    [SerializeField]
    private bool isDefaultCar = false;

    [Space, Header("Car visual settings")]
    [SerializeField]
    private Material revealedMaterial;
    [SerializeField]
    private Material unrevealedMaterial;
    [SerializeField]
    private List<MeshRenderer> carParts;

    public void LoadCarData()
    {
        if (isDefaultCar)
        {
            carData.unlocked = true;
            carData.revealed = true;

            return;
        }

        carData.unlocked = PlayerPrefsUtility.GetCarUnlockedState($"Unlocked_{carData.name}");
        carData.revealed = PlayerPrefsUtility.GetCarRevealedState($"Revealed_{carData.name}");

        if (!carData.revealed)
            ChangeCarMaterial(unrevealedMaterial);
    }

    private void ChangeCarMaterial(Material newMaterial)
    {
        foreach (MeshRenderer part in carParts)
            part.material = newMaterial;
    }

    /// <summary>
    /// Change the current CarData unlocked state to true and saves it on PlayerPrefs
    /// </summary>
    public void UnlockCar()
    {
        carData.unlocked = true;
        carData.revealed = true;

        PlayerPrefsUtility.SetCarUnlockedState($"Unlocked_{carData.name}", 1);
        PlayerPrefsUtility.SetCarRevealedState($"Revealed_{carData.name}", 1);

        RevealCar();
    }

    /// <summary>
    /// Shows the true apparience of the car
    /// </summary>
    public void RevealCar()
    {
        ChangeCarMaterial(revealedMaterial);

        carData.revealed = true;
        PlayerPrefsUtility.SetCarRevealedState($"Revealed_{carData.name}", 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>CarData from current car</returns>
    public CarData GetCarData() => carData;
}
