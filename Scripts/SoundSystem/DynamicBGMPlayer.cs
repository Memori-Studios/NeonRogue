using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TJAudio
{
public class DynamicBGMPlayer : MonoBehaviour
{
    [Range(0f,1f)] [SerializeField] private float musicScale;
    [Range(0f,1f)] [SerializeField] private float effectScale;

    [SerializeField] private float musicVolume, transitionTime;
    [SerializeField] private AudioSource audioSourceA, audioSourceB;
    [SerializeField] private bool musicSwichInProgress;
    [SerializeField] private SerializableDictionary<string, AudioClip> audioClipsPool = new SerializableDictionary<string, AudioClip>();
    
    public void SwitchBGM(string clipName)
    {
        AudioClip clipRequested = AudioClipExists(clipName);

        if(clipRequested == null)
        {
            #if UNITY_EDITOR
                Debug.Log($"Clip was not found");
            #endif
            return;
        }

        if(musicSwichInProgress)
        {
            //need to add some sort of quick fade thing if we try to swap before it has been completed, not sure how yet
        }

        musicSwichInProgress = true;

        if(audioSourceA.isPlaying)
        {
            audioSourceB.clip = clipRequested;
            StartCoroutine(FadeAudio(audioSourceA, audioSourceA.volume, 0, transitionTime));
            StartCoroutine(FadeAudio(audioSourceB, audioSourceB.volume, musicVolume, transitionTime));
        }
        else
        {
            audioSourceB.clip = clipRequested;
            StartCoroutine(FadeAudio(audioSourceB, audioSourceB.volume, 0, transitionTime));
            StartCoroutine(FadeAudio(audioSourceA, audioSourceA.volume, musicVolume, transitionTime));
        }

        Invoke("CompleteTransition", transitionTime);
    }
    private IEnumerator FadeAudio(AudioSource audioSource, float startValue, float endValue, float fadeDuration)
    {
        if(audioSource.volume == 0)
            audioSource.Play();

        float currentLerpTime = 0f, lerpValue = 0f;
  
        while (audioSource.volume != endValue)
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime < fadeDuration)
            {
                lerpValue = Mathf.Lerp(startValue, endValue, currentLerpTime / fadeDuration);
            }
            else
            {
                lerpValue = endValue;
            }

            audioSource.volume= lerpValue;
            yield return null;
        }

        if(audioSource.volume == 0)
            audioSource.Pause();
    }
    private void CompleteTransition()
    {
        musicSwichInProgress = false;
    }
    private AudioClip AudioClipExists(string clipName)
    {
        return audioClipsPool.TryGetValue(clipName, out var clipRequested) ? clipRequested : default;
    }
}
}