using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource1; // First audio source
    public AudioSource audioSource2; // Second audio source

    void Awake()
    {
        // Play the first audio source on awake
        audioSource1.Play();
    }

    void Update()
    {
        // Check if the first audio source is not playing
        if (!audioSource1.isPlaying && !audioSource2.isPlaying)
        {
            // Play the second audio source if it's not already playing
            audioSource2.Play();
            // Optionally, you can disable this script after playing the second audio
            // enabled = false; 
        }
    }
}

