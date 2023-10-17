using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarUnlockManager : MonoBehaviour
{
    [Space, SerializeField]
    private UnityEvent onCarPurchaseCompleted;
    [Space, SerializeField]
    private UnityEvent onCarPurchaseDenied;

    private void Awake() => SingletonManager.Managers.carUnlockManager = this;

    public void RequestCarPurchase(CarData carData, UnlockableCar unlockableCar)
    {
        int playerPoints = PlayerPrefsUtility.GetPlayerPoints();

        if (playerPoints > carData.price)
        {
            SingletonManager.Managers.pointsManager.ReducePoints(carData.price);
            unlockableCar.UnlockCar();

            onCarPurchaseCompleted.Invoke();
        }
        else
        {
            onCarPurchaseDenied.Invoke();
        }
    }
}
