using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private TextMeshProUGUI SFXText;

    [SerializeField] private Slider volumeSilder;
    [SerializeField] private Slider SFXSlider;

    [SerializeField] MusicManager musicManager;
    [SerializeField] SoundManager soundManager;

    [SerializeField] private Button closeButton;

   


    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            LoadSFX();
        }
        else
        {
            LoadSFX();
        }

        HideOptionsUI();

        
    }

    private void Awake()
    {
        closeButton.onClick.AddListener(HideOptionsUI);
    }

    public void OnVolumeSliderChanged(float volume)
    {
       
        volumeText.text = volume.ToString();
        musicManager.ChangeVolume(volume);
        SaveVolume(volume);
    }

    public void OnSFXSliderChanged(float volume)
    {

        SFXText.text = volume.ToString();
        soundManager.ChangeVolume(volume);
        SaveSFX(volume);
    }

    private void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        volumeSilder.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveSFX(float volume)
    {
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void ShowOptionsUI()
    {
        gameObject.SetActive(true);
    }

    public void HideOptionsUI()
    {
        gameObject.SetActive(false);
    }

}
