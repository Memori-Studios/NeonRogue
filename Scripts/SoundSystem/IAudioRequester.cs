using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJAudio;
namespace TJAudio
{
public class IAudioRequester : MonoBehaviour
{
    public static IAudioRequester instance;
    [SerializeField] private FixedBGMPlayer fixedBGMPlayer;
    [SerializeField] private DynamicBGMPlayer dynamicBGMPlayer;
    [SerializeField] public AmbiencePlayer ambiencePlayer;
    [SerializeField] private SFXPlayer sFXPlayer;
    private void Awake() 
    {
        if(instance==null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GameEvents.instance.onButtonClicked += OnButtonClicked;
        GameEvents.instance.onClearTreesButtonClicked += OnClearTreesButtonClicked;
        GameEvents.instance.onOpenUI += OnOpenUI;
        GameEvents.instance.onProcessWoodClicked += OnProcessWood;
    }
    private void OnButtonClicked()
    {
        instance.PlaySFX("mouseClick");
    }
    private void OnClearTreesButtonClicked()
    {
        instance.PlaySFX("startBuild");
    }
    private void OnProcessWood()
    {
        instance.PlaySFX("startBuild");
    }
    private void OnOpenUI(bool openUI)
    {
        if(openUI)
            instance.PlaySFX("openUI");
        else
            instance.PlaySFX("closeUI");
    }
    public void PlaySFX(string clipName)
    {
        sFXPlayer.PlaySFX(clipName);
    }
    public void SetBGMVolumeLevel(float value)
    {
        fixedBGMPlayer.SetVolumeLevel(value);
    }
    public void SetSFXVolumeLevel(float value)
    {
        sFXPlayer.SetVolumeLevel(value);
    }
    public static void PlaySFX_Static(string clipName)
    {
        instance.PlaySFX(clipName);
    }
}
public struct AudioClipID
{
    public string id;
    public AudioClip audioClip;
}
}

