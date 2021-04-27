using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Slider musicSlider = null;

    const string VOLUME = "volume";
    const string MUSIC = "music";

    private void Awake()
    {
        InitializeVolume();
    }

    private void InitializeVolume()
    {
        if (!PlayerPrefs.HasKey(VOLUME) || !PlayerPrefs.HasKey(MUSIC))
        {
            PlayerPrefs.SetFloat(VOLUME, 0.8f);
            PlayerPrefs.SetFloat(MUSIC, 0.8f);
        }
        else
        {
            return;
        }
    }

    public void SaveAndContinue()
    {
        PlayerPrefs.SetFloat(VOLUME, volumeSlider.value);
        PlayerPrefs.SetFloat(MUSIC, musicSlider.value);
        FindObjectOfType<MusicManager>().ChangeMusicVolume();
    }

    public void UpdateDisplay()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(VOLUME);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC);
    }
}
