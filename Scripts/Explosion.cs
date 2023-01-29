using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SphereCollider sphereCollider;
    [SerializeField] private float damage = 10f;
    private void Start()
    {
        StartCoroutine(Flash());
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name} hit");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Player hit");
            PlayerManager.instance.TakeDamage(damage);
        }
    }
    public IEnumerator Flash()
    {
        sphereCollider.enabled = true;
        Debug.Log($"EXPLODING   ");
        yield return new WaitForSeconds(0.1f);
        sphereCollider.enabled = false;
    }
}
