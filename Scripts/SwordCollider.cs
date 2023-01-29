using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    [SerializeField] private float damage;
    public string tagToHit;
    public Collider collider;
    public void SetStats(float _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==tagToHit)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
        }
    }
}
