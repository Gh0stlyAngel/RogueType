using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        StartCoroutine(DelayedVolumeApply());
    }

    private IEnumerator DelayedVolumeApply()
    {
        yield return null; // wait 1 frame
        LoadVolume("masterVolume", "masterVolume", masterVolumeSlider);
        LoadVolume("soundFXVolume", "soundFXVolume", soundFXVolumeSlider);
        LoadVolume("musicVolume", "musicVolume", musicVolumeSlider);
    }

    public void SetMasterVolume(float level)
    {
        mainAudioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("masterVolume", level);
        PlayerPrefs.Save();
    }

    public void SetSoundFXVolume(float level)
    {
        mainAudioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("soundFXVolume", level);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float level)
    {
        mainAudioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("musicVolume", level);
        PlayerPrefs.Save();
    }

    private void LoadVolume(string parameterKey, string parameterName, Slider slider)
    {
        float level = PlayerPrefs.GetFloat(parameterKey, 1);
        slider.value = level;
        mainAudioMixer.SetFloat(parameterName, Mathf.Log10(level) * 20f);
    }
}
