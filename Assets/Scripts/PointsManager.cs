using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField]
    private int playerPoints = 0; 

    private void Start()
    {
        SingletonManager.PointsSystem.pointsManager = this;
        playerPoints = PlayerPrefsUtility.GetPlayerPoints();
        SingletonManager.PointsSystem.matchPoints = 0;
    }
}
