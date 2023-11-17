using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Audio settings")]
    [SerializeField]
    private AudioMixer audioMixer;

    private const string MASTER_VOLUME_NAME = "masterVolume";
    private const string SFX_VOLUME_NAME = "musicVolume";
    private const string MUSIC_VOLUME_NAME = "SFXVolume";

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
}
