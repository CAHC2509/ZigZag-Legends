using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI actualScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    private int actualScore = 0;

    private void Start()
    {
        SingleInstanceManager.Managers.highScoreManager = this;

        actualScoreText.text = $"Actual: {actualScore}m";
        highScoreText.text = $"Best: {PlayerPrefsUtility.GetHighScore()}m";
    }

    public void CheckHighScore()
    {
        int actualHighScore = PlayerPrefsUtility.GetHighScore();

        if (actualScore > actualHighScore)
            UpdateHighScore(actualScore);
    }

    public void UpdateActualScore()
    {
        actualScore++;
        actualScoreText.text = $"Actual: {actualScore}m";
    }

    public void UpdateHighScore(int newHighScore)
    {
        PlayerPrefsUtility.SetHighScore(newHighScore);
        highScoreText.text = $"Best: {newHighScore}m";
    }

    public void ResetActualScore()
    {
        actualScore = 0;
        actualScoreText.text = $"Actual: {actualScore}m";
    }
}
