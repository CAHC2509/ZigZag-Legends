using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsText;

    public int playerPoints = 0; 

    private void Start()
    {
        SingletonManager.PointsSystem.pointsManager = this;
        playerPoints = PlayerPrefsUtility.GetPlayerPoints();
        pointsText.text = playerPoints.ToString();
    }

    public void AddPoints(int pointsAmount = 1)
    {
        playerPoints += pointsAmount;
        PlayerPrefsUtility.SetPlayerPoints(playerPoints);
        pointsText.text = PlayerPrefsUtility.GetPlayerPoints().ToString();
    }
}
