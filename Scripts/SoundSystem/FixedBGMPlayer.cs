using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJAudio
{
public class FixedBGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicElement defaultBGM;

    private void Start()
    {
        StartCoroutine(PlayClips(defaultBGM));
    }
    public IEnumerator PlayClips(MusicElement musicElement)
    {
        audioSource.clip  = musicElement.introClip;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip  = musicElement.loopClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void SetVolumeLevel(float value)
    {
        audioSource.volume = value;
    }
}
}