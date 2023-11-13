using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private Animator textAnimator;

    public int playerPoints = 0; 

    private void Start()
    {
        SingleInstanceManager.Managers.pointsManager = this;
        playerPoints = PlayerPrefsUtility.GetPlayerPoints();
        pointsText.text = playerPoints.ToString();
    }

    public void AddPoints(int pointsAmount = 1)
    {
        playerPoints += pointsAmount;
        PlayerPrefsUtility.SetPlayerPoints(playerPoints);
        pointsText.text = PlayerPrefsUtility.GetPlayerPoints().ToString();

        textAnimator.SetTrigger("PointsEarned");
    }

    public void ReducePoints(int pointsAmount)
    {
        playerPoints -= pointsAmount;
        PlayerPrefsUtility.SetPlayerPoints(playerPoints);
        pointsText.text = PlayerPrefsUtility.GetPlayerPoints().ToString();

        textAnimator.SetTrigger("PointsEarned");
    }
}
