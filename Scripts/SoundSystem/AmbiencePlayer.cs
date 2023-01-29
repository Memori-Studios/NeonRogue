using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TJAudio
{
public class AmbiencePlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsPool;
    [SerializeField] private int timeIntervalToSelectClips = 5;
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> freedAudioSources = new List<AudioSource>();
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private int maxAmountOfAudioSources = 5;
    [SerializeField] private bool turnOff;
    private float timer;
    public event Action OnAudioEndPlaying;

    public void TurnOff(bool stopAllAudioSources)
    {
        //prevent playing new audio
        turnOff = true;

        //stop playing all audio
        if(stopAllAudioSources)
            CancelInvoke("PlayAmbiantClip");
    }
    public void OnAudioSourcePlayingHandler()
    {
        if(OnAudioEndPlaying!=null)
            OnAudioEndPlaying();
    }
    public void OnAudioSourceFreedHandler(AudioSource audioSource)
    {
        freedAudioSources.Add(audioSource);
    }
    private void FixedUpdate()
    {
        if(turnOff)
            return;
        
        timer += Time.deltaTime;

        if(timer >= timeIntervalToSelectClips)
        {
            timer = 0;
            PlayAmbiantClip();
        }
    }
    public void PlayAmbiantClip()
    {       
        AudioSource freeAudioSource = GetFreeAudioSource(); 
        if(freeAudioSource == null)
            freeAudioSource = AddAudioSource();

        if(freeAudioSource == null)
        {
            //no free audio source available and max amount of audio sources reached
            return;
        }

        freeAudioSource.clip = SelectRandomAudioClip();
        freeAudioSource.Play();
        // Debug.Log($"Playing {freeAudioSource.clip.name} on {freeAudioSource.gameObject.name}");
    }
    private AudioSource AddAudioSource()
    {
        if(audioSources.Count >= maxAmountOfAudioSources)
        {
            #if UNITY_EDITOR
            // Debug.Log("Max amount of audio sources reached");
            #endif

            return null;
        }
        
        GameObject newAudioSource = Instantiate(audioSourcePrefab, transform);
        AudioSource newAudioSourceComponent = newAudioSource.GetComponent<AudioSource>();
        audioSources.Add(newAudioSourceComponent);
        return newAudioSourceComponent;
    }
    private AudioClip SelectRandomAudioClip()
    {
        return audioClipsPool[UnityEngine.Random.Range(0, audioClipsPool.Length)];
    }
    private AudioSource GetFreeAudioSource()
    {
        for(int i = 0; i < freedAudioSources.Count;)
        {
            AudioSource freeAudioSource = freedAudioSources[i];
            freedAudioSources.RemoveAt(i);
            return freeAudioSource;
        }
        return null;
    }
}
}