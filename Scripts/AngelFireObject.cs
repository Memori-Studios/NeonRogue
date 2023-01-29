using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelFireObject : MonoBehaviour
{
    public GameObject impactPrefab, beamPrefab;
    public float damage = 1f;
    public SphereCollider explosionCollider;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        impactPrefab.SetActive(true);
        beamPrefab.SetActive(false);
        explosionCollider.enabled = true;
        this.gameObject.transform.parent = null;
        Destroy(gameObject, 3f);
    }
    public GameObject SetUpAngelFire(int damage, float radius)
    {
        this.damage = damage;
        explosionCollider.radius = radius;
        return this.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }
}
