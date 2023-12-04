using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RewardedAdsButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [Space, SerializeField]
    private UnityEvent onAdvertisementCompleted;

    private void Start()
    {
        if (button == null)
            gameObject.GetComponent<Button>();
    }

    /// <summary>
    /// Sends the corresponding UnityEvent to execute when the ad is completed
    /// </summary>
    public void TryToShowAd() => RewardedAds.instance.ShowAd(onAdvertisementCompleted, button);
}