using UnityEngine;

/// <summary>
/// Save and load different data using PlayerPrefs
/// </summary>
public static class PlayerPrefsUtility
{
    private const string PLAYERPOINTSKEY = "PlayerPoints";

    /// <summary>
    /// Add more points to the existing in PlayerPrefs
    /// </summary>
    /// <param name="pointsToAdd"></param>
    public static void AddPlayerPoints(int pointsToAdd)
    {
        pointsToAdd += GetPlayerPoints();
        SetPlayerPoints(pointsToAdd);
    }

    /// <summary>
    /// Set a new points value and saves it in PlayerPrefs
    /// </summary>
    /// <param name="newPoints"></param>
    public static void SetPlayerPoints(int newPoints) => PlayerPrefs.SetInt(PLAYERPOINTSKEY, newPoints);

    /// <summary>
    /// Get the player's points amount from the PlayerPrefs
    /// </summary>
    /// <returns>Player's points amount</returns>
    public static int GetPlayerPoints() => PlayerPrefs.GetInt(PLAYERPOINTSKEY, 0);
}
