using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource hurtSoundFXObject;
    [SerializeField] private AudioSource collectSoundFXObject;

    public AudioSource BGMusicSource;

    [SerializeField] private AudioClip BGMusic;

    [SerializeField] private AudioClip hurtAudioClip;

    [SerializeField] private AudioSourcePool SoundFXAudioSourcePool;
    [SerializeField] private AudioSourcePool HurtSFXAudioSourcePool;
    [SerializeField] private AudioSourcePool CollectSFXAudioSourcePool;

    [SerializeField] private HashSet<AudioSource> activeAudioSources = new HashSet<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        PlayBGMusic(0.2f);
    }
    private void HandleSourceFinished(AudioSource source)
    {
        activeAudioSources.Remove(source);
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform audioTansform, float volume)
    {
        var source = SoundFXAudioSourcePool.PlayAtPosition(audioClip, volume, audioTansform.position);
        activeAudioSources.Add(source.GetAudioSource());
        source.OnFinishedPlaying += HandleSourceFinished;
    }

    public void PlayHurtSoundFXClip(Transform audioTansform)
    {

        HurtSFXAudioSourcePool.PlayAtPosition(hurtAudioClip, 0.05f, audioTansform.position);

    }

    public void PlayCollectSoundFXClip(AudioClip audioClip, Transform audioTansform, float volume)
    {
        CollectSFXAudioSourcePool.PlayAtPosition(audioClip, volume, audioTansform.position);
    }

    public void PlaySoundFXClipWithoutStopping(AudioClip audioClip, Transform audioTansform, float volume)
    {
        SoundFXAudioSourcePool.PlayAtPosition(audioClip, volume, audioTansform.position);
    }

    private void PlayBGMusic(float volume)
    {
        BGMusicSource.clip = BGMusic;
        BGMusicSource.volume = volume;
        BGMusicSource.loop = true;
        BGMusicSource.Play();

    }

    public void PauseActiveSFX()
    {
        foreach (AudioSource audioSource in activeAudioSources)
        {
            audioSource.Pause();
        }
    }

    public void ResumeActiveSFX()
    {
        foreach(AudioSource audioSource in activeAudioSources)
        {
            audioSource.UnPause();
        }
    }
}
