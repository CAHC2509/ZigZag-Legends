using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Animator textAnimator;

    public int playerPoints = 0;

    /// <summary>
    /// Initialize singleton instance and set up default values.
    /// </summary>
    private void Start()
    {
        SingleInstanceManager.Managers.pointsManager = this;
        playerPoints = PlayerPrefsUtility.GetPlayerPoints();
        pointsText.text = playerPoints.ToString();
    }

    /// <summary>
    /// Adds points to the player's total, updates UI text, and triggers the points earned animation.
    /// </summary>
    /// <param name="pointsAmount">The amount of points to add (default is 1).</param>
    public void AddPoints(int pointsAmount = 1)
    {
        playerPoints += pointsAmount;
        PlayerPrefsUtility.SetPlayerPoints(playerPoints);
        pointsText.text = PlayerPrefsUtility.GetPlayerPoints().ToString();

        textAnimator.SetTrigger("PointsEarned");
    }

    /// <summary>
    /// Reduces points from the player's total, updates UI text, and triggers the points earned animation.
    /// </summary>
    /// <param name="pointsAmount">The amount of points to reduce.</param>
    public void ReducePoints(int pointsAmount)
    {
        playerPoints -= pointsAmount;
        PlayerPrefsUtility.SetPlayerPoints(playerPoints);
        pointsText.text = PlayerPrefsUtility.GetPlayerPoints().ToString();

        textAnimator.SetTrigger("PointsEarned");
    }
}
