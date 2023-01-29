using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="ScriptableObjects/ExpShard")]
public class ExpShard : ScriptableObject
{
    public GameObject shardPrefab;
    public float expAmount;
}
