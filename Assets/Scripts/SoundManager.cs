using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    [Space, Header("Music settings")]
    [SerializeField]
    private AudioSource menuMusicSource;
    [SerializeField]
    private AudioSource bucleMusicSource;

    [Space, Header("FX settings")]
    [SerializeField]
    private AudioSource tapSFX;
    [SerializeField]
    private AudioSource clickSFX;
    [SerializeField]
    private AudioSource coinSFX;

    private const string MASTER_VOLUME_NAME = "masterVolume";
    private const string MUSIC_VOLUME_NAME = "musicVolume";
    private const string SFX_VOLUME_NAME = "SFXVolume";

    private bool inMatch = false;

    public static SoundManager instance;

    private void Awake() => instance = this;

    private void Start()
    {
        // Get stored values from PlayerPrefs
        float masterVolume = PlayerPrefsUtility.GetVolume(MASTER_VOLUME_NAME);
        float musicVolume = PlayerPrefsUtility.GetVolume(MUSIC_VOLUME_NAME);
        float sfxVolume = PlayerPrefsUtility.GetVolume(SFX_VOLUME_NAME);

        // Set the audio mixer volume based on the last value saved
        audioMixer.SetFloat(MASTER_VOLUME_NAME, masterVolume);
        audioMixer.SetFloat(MUSIC_VOLUME_NAME, musicVolume);
        audioMixer.SetFloat(SFX_VOLUME_NAME, sfxVolume);

        // Set the sliders value based on the last saved
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    private void Update()
    {
        if (!inMatch)
        {
            if (!menuMusicSource.isPlaying && !bucleMusicSource.isPlaying)
                bucleMusicSource.gameObject.SetActive(true);
        }
    }

    #region Music

    public void ChangeInMatchState(bool state) => inMatch = state;

    #endregion

    #region SFX

    public void PlayTapSFX() => tapSFX?.Play();

    public void PlayClickSFX() => clickSFX?.Play();

    public void PlayCoinSFX() => coinSFX?.Play();

    #endregion

    #region Audio settings

    /// <summary>
    /// Set a new float value for the master volume
    /// </summary>
    /// <param name="value">New value</param>
    public void ChangeMasterVolume(float value)
    {
        audioMixer.SetFloat(MASTER_VOLUME_NAME, value);
        PlayerPrefsUtility.SetVolume(MASTER_VOLUME_NAME, value);
    }

    /// <summary>
    /// Set a new float value for the music volume
    /// </summary>
    /// <param name="value">New value</param>
    public void ChangeMusicVolume(float value)
    {
        audioMixer.SetFloat(MUSIC_VOLUME_NAME, value);
        PlayerPrefsUtility.SetVolume(MUSIC_VOLUME_NAME, value);
    }

    /// <summary>
    /// Set a new float value for the SFX volume
    /// </summary>
    /// <param name="value">New value</param>
    public void ChangeSFXVolume(float value)
    {
        audioMixer.SetFloat(SFX_VOLUME_NAME, value);
        PlayerPrefsUtility.SetVolume(SFX_VOLUME_NAME, value);
    }

    #endregion
}
