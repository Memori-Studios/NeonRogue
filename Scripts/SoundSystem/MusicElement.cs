using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJAudio
{
[CreateAssetMenu(menuName = "ScriptableObjects/MusicElement")]
public class MusicElement : ScriptableObject
{
    public AudioClip introClip, loopClip;
}
}