using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;
    private void Reset()
    {
        if(audioSource == null)
        {
            audioSource = FindObjectOfType<Canvas>()?.GetComponent<AudioSource>();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound, 0.1f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, 0.1f);
        }
        
    }


}
