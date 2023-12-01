using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField]
    private AudioMixer audioMixer;

    [Space, Header("Music settings")]
    [SerializeField]
    private AudioSource menuMusicSource;
    [SerializeField]
    private AudioSource bucleMusicSource;

    [Space, Header("FX settings")]
    [SerializeField]
    private AudioSource coinSFX;

    private const string MASTER_VOLUME_NAME = "masterVolume";
    private const string SFX_VOLUME_NAME = "musicVolume";
    private const string MUSIC_VOLUME_NAME = "SFXVolume";

    private bool inMatch = false;

    public static SoundManager instance;

    private void Awake() => instance = this;

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

    public void PlayCoinSFX() => coinSFX?.Play();

    #endregion

    #region Audio settings

    /// <summary>
    /// Set a new float value for the master volume
    /// </summary>
    /// <param name="value">New value</param>
    public void ChangeMasterVolume(float value) => audioMixer.SetFloat(MASTER_VOLUME_NAME, value);

    /// <summary>
    /// Set a new float value for the music volume
    /// </summary>
    /// <param name="value">New value</param>
    public void ChangeMusicVolume(float value) => audioMixer.SetFloat(MUSIC_VOLUME_NAME, value);

    /// <summary>
    /// Set a new float value for the SFX volume
    /// </summary>
    /// <param name="value">New value</param>
    public void ChangeSFXVolume(float value) => audioMixer.SetFloat(SFX_VOLUME_NAME, value);

    #endregion
}
