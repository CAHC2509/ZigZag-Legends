using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    private List<GameObject> showAdButtons;
    [SerializeField]
    private GameObject errorWithAdPopUp;

    private UnityEvent onAdvertisementCompleted;
    private Button adButtonPressed;
    private string _adUnitId = "Rewarded_Android";

    public static RewardedAds instance;

    void Awake() => instance = this;

    /// <summary>
    /// Changes the active state of the rewarded ad buttons.
    /// </summary>
    /// <param name="newState">The new state to set for the buttons.</param>
    public void ChangeButtonsActiveState(bool newState)
    {
        foreach (GameObject button in showAdButtons)
            button.SetActive(newState);
    }

    /// <summary>
    /// Loads a rewarded ad.
    /// </summary>
    public void LoadAd()
    {
        Debug.Log($"Loading Ad: {transform.name}");
        Advertisement.Load(_adUnitId, this);
    }

    /// <summary>
    /// Callback when a rewarded ad is successfully loaded.
    /// </summary>
    /// <param name="adUnitId">The ID of the ad unit.</param>
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + transform.name);
    }

    /// <summary>
    /// Shows a rewarded ad.
    /// </summary>
    /// <param name="unityEvent">The UnityEvent to invoke when the ad is completed.</param>
    /// <param name="button">The button associated with the ad.</param>
    public void ShowAd(UnityEvent unityEvent, Button button)
    {
        onAdvertisementCompleted = unityEvent;
        adButtonPressed = button;

        adButtonPressed.interactable = false;

        Advertisement.Show(_adUnitId, this);
    }

    /// <summary>
    /// Callback when a rewarded ad is completed.
    /// </summary>
    /// <param name="adUnitId">The ID of the ad unit.</param>
    /// <param name="showCompletionState">The completion state of the ad show.</param>
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");

            onAdvertisementCompleted?.Invoke();

            LoadAd();

            errorWithAdPopUp.SetActive(false);
            adButtonPressed.interactable = true;
        }
    }

    /// <summary>
    /// Callback when loading a rewarded ad fails.
    /// </summary>
    /// <param name="adUnitId">The ID of the ad unit.</param>
    /// <param name="error">The error type.</param>
    /// <param name="message">The error message.</param>
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error} - {message}");
    }

    /// <summary>
    /// Callback when showing a rewarded ad fails.
    /// </summary>
    /// <param name="adUnitId">The ID of the ad unit.</param>
    /// <param name="error">The error type.</param>
    /// <param name="message">The error message.</param>
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}");

        errorWithAdPopUp.SetActive(true);
        adButtonPressed.interactable = true;
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
