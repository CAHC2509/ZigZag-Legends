using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Ads settings")]
    [SerializeField]
    private InterstitialAds interstitialAds;
    [SerializeField]
    private RewardedAds rewardedAds;
    [SerializeField]
    string _androidGameId;
    [SerializeField]
    bool _testMode = true;

    private string _gameId;

    public static bool adsInitialized = false;

    private void Start() => StartCoroutine(CheckInternet());

    // Initialize the Unity Ads SDK.
    public void InitializeAds()
    {
#if UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; // Only for testing the functionality in the Editor
#endif

        // Check if Unity Ads is not initialized and is supported
        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, _testMode, this);
    }

    // Coroutine to check if the device has an active internet connection before initializing ads.
    private IEnumerator CheckInternet()
    {
        UnityWebRequest www = new UnityWebRequest("http://www.google.com");
        DownloadHandler dh = new DownloadHandlerBuffer();
        www.downloadHandler = dh;

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Internet connection available.");

            // Initialize ads if internet connection is available
            InitializeAds();
        }
        else
        {
            Debug.Log("No internet connection available.");

            // Disable rewarded ads buttons if no internet connection
            rewardedAds.ChangeButtonsActiveState(false);
        }
    }

    /// <summary>
    /// Callback when Unity Ads initialization is complete.
    /// </summary>
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        // Enable rewarded ads buttons and load ads
        rewardedAds.ChangeButtonsActiveState(true);

        interstitialAds.LoadAd();
        rewardedAds.LoadAd();

        adsInitialized = true;
    }

    /// <summary>
    /// Callback when Unity Ads initialization fails.
    /// </summary>
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // Disable rewarded ads buttons and set adsInitialized to false on initialization failure
        rewardedAds.ChangeButtonsActiveState(false);
        adsInitialized = false;

        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }
}
