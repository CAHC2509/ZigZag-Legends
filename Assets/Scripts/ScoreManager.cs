using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("In match texts")]
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [Space, Header("Death screen texts")]
    [SerializeField]
    private TextMeshProUGUI traveledText;
    [SerializeField]
    private TextMeshProUGUI recordText;
    [SerializeField]
    private TextMeshProUGUI pointsCollectedText;
    [SerializeField]
    private Color newRecordColor;

    private Color recordDefaultColor;
    private int currentScore = 0;
    private bool newRecord;

    public static ScoreManager instance;
    public static int pointsCollected = 0;

    private void Awake() => instance = this;

    private void Start()
    {
        recordDefaultColor = recordText.color;

        currentScoreText.text = $"Current: {currentScore}m";
        highScoreText.text = $"Best: {PlayerPrefsUtility.GetHighScore()}m";
    }

    public void UpdateDeathScreenTexts()
    {
        traveledText.text = $"Traveled: {currentScore}m";
        recordText.text = $"Record: {PlayerPrefsUtility.GetHighScore()}m";
        pointsCollectedText.text = $"Points collected: {pointsCollected}";

        if (newRecord)
        {
            recordText.color = newRecordColor;
            recordText.text = $"New record: {PlayerPrefsUtility.GetHighScore()}m";
            newRecord = false;
        }
    }

    public void CheckHighScore()
    {
        int actualHighScore = PlayerPrefsUtility.GetHighScore();

        if (currentScore > actualHighScore)
        {
            newRecord = true;
            UpdateHighScore(currentScore);
        }
    }

    public void UpdateCurrentScore()
    {
        currentScore++;
        currentScoreText.text = $"Current: {currentScore}m";
    }

    public void UpdateHighScore(int newHighScore)
    {
        PlayerPrefsUtility.SetHighScore(newHighScore);
        highScoreText.text = $"Best: {newHighScore}m";
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        pointsCollected = 0;
        currentScoreText.text = $"Current: {currentScore}m";
        recordText.color = recordDefaultColor;
    }
}
