using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("In match texts")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Space, Header("Death screen texts")]
    [SerializeField] private TextMeshProUGUI traveledText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private TextMeshProUGUI pointsCollectedText;
    [SerializeField] private Color newRecordColor;

    private Color textsDefaultColor;
    private int currentScore = 0;
    private bool newRecord;

    public static ScoreManager instance;
    public static int pointsCollected = 0;

    /// <summary>
    /// Initialize singleton instance and set up default values.
    /// </summary>
    private void Awake() => instance = this;

    /// <summary>
    /// Initialize UI texts with default values.
    /// </summary>
    private void Start()
    {
        textsDefaultColor = recordText.color;

        currentScoreText.text = $"Current: {currentScore}m";
        highScoreText.text = $"Best: {PlayerPrefsUtility.GetHighScore()}m";
    }

    /// <summary>
    /// Updates the death screen UI texts with relevant information.
    /// </summary>
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

    /// <summary>
    /// Checks if the current score surpasses the high score and updates accordingly.
    /// </summary>
    public void CheckHighScore()
    {
        int actualHighScore = PlayerPrefsUtility.GetHighScore();

        if (currentScore > actualHighScore)
        {
            newRecord = true;
            UpdateHighScore(currentScore);
        }
    }

    /// <summary>
    /// Doubles the points collected during the match and updates UI text accordingly.
    /// </summary>
    public void DuplicateMatchPoints()
    {
        if (pointsCollected > 0)
        {
            SingleInstanceManager.Managers.pointsManager.AddPoints(pointsCollected);
            pointsCollectedText.text = $"Points collected: {pointsCollected * 2}";
        }
        else
        {
            SingleInstanceManager.Managers.pointsManager.AddPoints();
            pointsCollectedText.text = $"Points collected: {1}";
        }

        pointsCollectedText.color = newRecordColor;
    }

    /// <summary>
    /// Updates the current score and corresponding UI text.
    /// </summary>
    public void UpdateCurrentScore()
    {
        currentScore++;
        currentScoreText.text = $"Current: {currentScore}m";
    }

    /// <summary>
    /// Updates the high score and corresponding UI text.
    /// </summary>
    public void UpdateHighScore(int newHighScore)
    {
        PlayerPrefsUtility.SetHighScore(newHighScore);
        highScoreText.text = $"Best: {newHighScore}m";
    }

    /// <summary>
    /// Resets the current score and points collected to zero and restores default text colors.
    /// </summary>
    public void ResetCurrentScore()
    {
        currentScore = 0;
        pointsCollected = 0;
        currentScoreText.text = $"Current: {currentScore}m";
        recordText.color = textsDefaultColor;
        pointsCollectedText.color = textsDefaultColor;
    }
}
