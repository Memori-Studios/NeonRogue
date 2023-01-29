using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] meshRenderers;
    private void Awake()
    {
        SkinnedMeshRenderer meshRenderer = meshRenderers[Random.Range(0, meshRenderers.Length)];
        meshRenderer.gameObject.SetActive(true);
        GetComponent<Enemy>().meshRenderer = meshRenderer;
    }
}
