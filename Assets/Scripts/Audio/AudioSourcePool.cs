using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private int poolSize = 25;
    //[SerializeField] private int poolSizeMax = 60;

    private Queue<PooledAudioSource> pool = new Queue<PooledAudioSource>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewSource();
        }
    }

    private PooledAudioSource CreateNewSource()
    {
        GameObject obj = Instantiate(audioSourcePrefab, transform);
        var pooled = obj.GetComponent<PooledAudioSource>();
        pooled.Init(this);
        obj.SetActive(false);
        pool.Enqueue(pooled);
        return pooled;
    }

    public PooledAudioSource Play(AudioClip clip, float volume)
    {
        PooledAudioSource source = pool.Count > 0 ? pool.Dequeue() : CreateNewSource();

        source.gameObject.SetActive(true);
        source.PlayClip(clip, volume);

        return source;
    }

    public PooledAudioSource PlayAtPosition(AudioClip clip, float volume, Vector3 position)
    {
        PooledAudioSource source = Play(clip, volume);

        source.gameObject.transform.position = position;
        return source;
    }

    public void ReturnToPool(PooledAudioSource source)
    {
        source.gameObject.SetActive(false);
        pool.Enqueue(source);
    }
}
