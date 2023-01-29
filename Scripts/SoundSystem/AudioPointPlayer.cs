using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJAudio
{
public class AudioPointPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private bool wasPlayingAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        IAudioRequester.instance.ambiencePlayer.OnAudioEndPlaying += OnAudioEndPlaying;
    }
    private void Update()
    {
        if(audioSource.isPlaying)
        {
            wasPlayingAudio = true;
        }
        else if(wasPlayingAudio)
        {
            wasPlayingAudio = false;
            OnAudioEndPlaying();
        }
    }
    public void OnAudioEndPlaying()
    {
        Debug.Log($"we're done playing {audioSource.clip.name}");
        IAudioRequester.instance.ambiencePlayer.OnAudioSourceFreedHandler(audioSource);
    }
}
}