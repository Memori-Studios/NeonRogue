using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    [SerializeField] protected float health;
    public float Health => health;
    protected bool dead;
    public event System.Action OnDeath;
    public event System.Action<float> OnDamage;

    protected virtual void Start(){
        health = startingHealth;
    }
    public void TakeDamage(float damage){
        // Debug.Log($"took damage");
        if(OnDamage != null)
            OnDamage(damage);
    }
    public void TakeHit(float damage, RaycastHit hit)
    {
        Debug.Log($"took hit");
        health -= damage;

        if(health<=0&&!dead){
            Die();
        }
    }
    public void Die(){
        dead = true;
        if(OnDeath != null)
            OnDeath();
    }
}
