using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSourcePool pool;

    public event Action<AudioSource> OnFinishedPlaying;

    public void Init(AudioSourcePool pool)
    {
        audioSource = GetComponent<AudioSource>();
        this.pool = pool;
    }

    public void PlayClip(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(WaitForReturn());
    }

    private IEnumerator WaitForReturn()
    {
        float timer = 0f;
        float timeout = audioSource.clip.length + 1.5f;
        bool hasStarted = false;

        while (!hasStarted && timer < timeout)
        {
            if(audioSource.isPlaying)
            {
                hasStarted = true;
            }
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        if (!hasStarted)
        {
            OnFinishedPlaying?.Invoke(audioSource);
            pool.ReturnToPool(this);
            yield break;
        }

        timer = 0f;

        while (timer < timeout)
        {
            if ((!audioSource.isPlaying && audioSource.time > 0f) ||
            audioSource.time >= audioSource.clip.length - 0.05f)
            {
                OnFinishedPlaying?.Invoke(audioSource);
                pool.ReturnToPool(this);
                yield break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Debug.LogWarning($"AudioSource timeout: clip '{audioSource.clip?.name}' не завершился корректно");
        OnFinishedPlaying?.Invoke(audioSource);
        pool.ReturnToPool(this);

        /*yield return new WaitUntil(() => audioSource.isPlaying);


        *//*yield return new WaitUntil(() =>
            audioSource.isPlaying &&  
            audioSource.time >= audioSource.clip.length - 0.05f
            );*//*

        yield return new WaitUntil(() =>
        (!audioSource.isPlaying && audioSource.time > 0f) ||
        audioSource.time >= audioSource.clip.length - 0.05f
        
    );
        OnFinishedPlaying?.Invoke(audioSource);
        pool.ReturnToPool(this);*/
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}
