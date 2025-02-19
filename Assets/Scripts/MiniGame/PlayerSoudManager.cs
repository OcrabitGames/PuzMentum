using System;
using UnityEngine;

public class PlayerSoudManager : MonoBehaviour
{
    public AudioClip PickUpSound;
    public AudioClip WinSound;
    
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickUpSound()
    {
        audioSource.PlayOneShot(PickUpSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(WinSound);
    }
}
