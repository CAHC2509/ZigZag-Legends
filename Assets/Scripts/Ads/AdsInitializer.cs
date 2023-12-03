using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections.Generic;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Ads settings")]
    [SerializeField]
    private List<RewardedAdsButton> rewardedAdsButtons;
    [SerializeField]
    private InterstitialAds interstitialAds;
    [SerializeField]
    private Transform doublePointsButton;
    [SerializeField]
    private Transform doublePointsButtonParent;
    [SerializeField]
    string _androidGameId;
    [SerializeField]
    bool _testMode = true;
    
    private string _gameId;

    void Awake() => InitializeAds();

    private void Start() => doublePointsButton.SetParent(null);

    public void InitializeAds()
    {
#if UNITY_ANDROID

        _gameId = _androidGameId;

#elif UNITY_EDITOR

            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        foreach (RewardedAdsButton button in rewardedAdsButtons)
            button.LoadAd();

        interstitialAds.LoadAd();

        doublePointsButton.SetParent(doublePointsButtonParent);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) => Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
}