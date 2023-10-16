using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CarUnlockManager : MonoBehaviour
{
    [Space, SerializeField]
    private UnityEvent onCarPurchaseCompleted;
    [Space, SerializeField]
    private UnityEvent onCarPurchaseDenied;

    private void Awake() => SingletonManager.Managers.carUnlockManager = this;

    public void RequestCarPurchase(CarData carData)
    {
        int playerPoints = PlayerPrefsUtility.GetPlayerPoints();

        if (playerPoints > carData.price)
            Debug.Log($"Can buy {carData.name}");
        else
            Debug.Log($"Can't buy {carData.name}");
    }
}
