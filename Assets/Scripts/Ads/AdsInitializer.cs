using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections.Generic;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Ads settings")]
    [SerializeField]
    private InterstitialAds interstitialAds;
    [SerializeField]
    string _androidGameId;
    [SerializeField]
    bool _testMode = true;

    [Space, Header("Buttons and parents")]
    [SerializeField]
    private Transform doublePointsButton;
    [SerializeField]
    private Transform doublePointsButtonParent;
    [Space, SerializeField]
    private Transform revealCarButton;
    [SerializeField]
    private Transform revealCarButtonParent;
    [SerializeField]
    private List<RewardedAdsButton> rewardedAdsButtons;

    private string _gameId;

    void Awake() => InitializeAds();

    private void Start()
    {
        doublePointsButton.SetParent(null);
        revealCarButton.SetParent(null);
    }

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
        revealCarButton.SetParent(revealCarButtonParent);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");

        doublePointsButton.SetParent(doublePointsButtonParent);
        revealCarButton.SetParent(revealCarButtonParent);
    }
}