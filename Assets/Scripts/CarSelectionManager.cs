using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject rightButton;
    [SerializeField]
    private List<GameObject> cars;

    private int currentCarIndex = 0; // Index of the currently car displayed

    private void Start()
    {
        UpdateButtonsVisibility();
    }

    /// <summary>
    /// Display the next car in the sequence if available.
    /// </summary>
    public void NextCar()
    {
        if (currentCarIndex < cars.Count - 1)
        {
            cars[currentCarIndex].SetActive(false);
            currentCarIndex++;
            cars[currentCarIndex].SetActive(true);

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
            cars[currentCarIndex].SetActive(false);
            currentCarIndex--;
            cars[currentCarIndex].SetActive(true);

            UpdateButtonsVisibility();
        }
    }

    /// <summary>
    /// Update the visibility of navigation buttons based on the current car index.
    /// </summary>
    private void UpdateButtonsVisibility()
    {
        leftButton.SetActive(currentCarIndex > 0);
        rightButton.SetActive(currentCarIndex < cars.Count - 1);
    }
}
