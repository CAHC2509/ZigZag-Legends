using UnityEngine;

/// <summary>
/// Save and load different data using PlayerPrefs
/// </summary>
public static class PlayerPrefsUtility
{
    private const string PLAYERPOINTSKEY = "PlayerPoints";
    private const string HIGHSCOREKEY = "PlayerHighScore";
    private const string CARSELECTEDKEY = "CarSelected";

    /// <summary>
    /// Add more points to the existing in PlayerPrefs
    /// </summary>
    /// <param name="pointsToAdd"></param>
    public static void AddPlayerPoints(int pointsToAdd)
    {
        pointsToAdd += GetPlayerPoints();
        SetPlayerPoints(pointsToAdd);
    }

    #region Player releated functions

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

    /// <summary>
    /// Set a new high score and saves it in PlayerPrefs
    /// </summary>
    /// <param name="newHighScore"></param>
    public static void SetHighScore(int newHighScore) => PlayerPrefs.SetInt(HIGHSCOREKEY, newHighScore);

    /// <summary>
    /// Get the player's high score from the PlayerPrefs
    /// </summary>
    /// <returns>Player's high score</returns>
    public static int GetHighScore() => PlayerPrefs.GetInt(HIGHSCOREKEY, 0);

    #endregion

    #region Cars releated functions

    /// <summary>
    /// Set a new car selected index and saves it in PlayerPrefs
    /// </summary>
    /// <param name="index"></param>
    public static void SetCarSelectedIndex(int index) => PlayerPrefs.SetInt(CARSELECTEDKEY, index);

    /// <summary>
    /// Get the car selected index from PlayerPrefs
    /// </summary>
    /// <returns></returns>
    public static int GetCarSelectedIndex() => PlayerPrefs.GetInt(CARSELECTEDKEY, 0);

    /// <summary>
    /// Set the car unlocked state and saves it in PlayerPrefss
    /// </summary>
    /// <param name="key">Car name</param>
    /// <param name="state">0 for locked, 1 for unlocked</param>
    public static void SetCarUnlockedState(string key, int state) => PlayerPrefs.SetInt(key, state);

    /// <summary>
    /// Get the car unlocked state from PlayerPrefs
    /// </summary>
    /// <param name="key">Car name</param>
    /// <returns>0 for locked, 1 for unlocked</returns>
    public static bool GetCarUnlockedState(string key) => PlayerPrefs.GetInt(key) == 1;

    /// <summary>
    /// Set the car revealed state and saves it in PlayerPrefss
    /// </summary>
    /// <param name="key">Car name</param>
    /// <param name="state">0 for revealed, 1 for unrevealed</param>
    public static void SetCarRevealedState(string key, int state) => PlayerPrefs.SetInt(key, state);

    /// <summary>
    /// Get the car revealed state from PlayerPrefs
    /// </summary>
    /// <param name="key">Car name</param>
    /// <returns>0 for revealed, 1 for unrevealed</returns>
    public static bool GetCarRevealedState(string key) => PlayerPrefs.GetInt(key) == 1;

    #endregion

    #region Settings releated functions

    /// <summary>
    /// Set a new volume value and saves it in PlayerPrefs
    /// </summary>
    public static void SetVolume(string key, float value) => PlayerPrefs.SetFloat(key, value);

    /// <summary>
    /// Get the volume value from PlayerPrefs
    /// </summary>
    public static float GetVolume (string key) => PlayerPrefs.GetFloat(key, -10f);

    #endregion
}
