using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJAudio
{
public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<string, AudioClip> audioClipsPool = new SerializableDictionary<string, AudioClip>();
    [SerializeField] private AudioSource sFXAudioSource;
    
    public void PlaySFX(string clipName)
    {
        AudioClip clipRequested = AudioClipExists(clipName);

        if(clipRequested == null)
        {
            #if UNITY_EDITOR
                Debug.Log($"Clip was not found");
            #endif
            return;
        }

        sFXAudioSource.PlayOneShot(clipRequested);
    }
    private AudioClip AudioClipExists(string clipName)
    {
        AudioClip clipRequested;
        if(audioClipsPool.TryGetValue(clipName, out clipRequested))
            return clipRequested;
        else
            return null;
    }
    public void SetVolumeLevel(float value)
    {
        sFXAudioSource.volume = value;
    }
}
}