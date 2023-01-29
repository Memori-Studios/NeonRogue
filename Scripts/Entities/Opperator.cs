using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opperator : Enemy
{
    [Header("Opperator Specific")]
    [SerializeField] private float muzzleVelocity = 2f;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float range = 10f;

    [Header("Weapon")]
    private Coroutine flashOnHit;
    private WaitForSeconds flashWait;
    [SerializeField] private Light muzzzleFlash;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Fire", 5f, 5f);
    }

    private void Fire()
    {
        StartCoroutine(FirePause());
    }
    private IEnumerator FirePause()
    {
        if(Vector3.Distance(transform.position, PlayerManager.instance.transform.position) > range)
            yield break;

        enemyMovementOverride = true;
        animator.Play("fireRifle");
        yield return new WaitForSeconds(1f);
        enemyMovementOverride = false;
        MuzzleFlash();

        // float offset = Random.Range(-projectileVariance, projectileVariance);
        this.transform.LookAt(PlayerManager.instance.transform.position);
        Quaternion newrotation = this.transform.rotation;
        newrotation *= Quaternion.Euler(0, 0, 0);
        Projectile newProjectile = Instantiate(projectile,this.transform.position+Vector3.up, newrotation) as Projectile;

        newProjectile.SetStats(muzzleVelocity, damage, 1, 0, 1);
    }
    public void MuzzleFlash()
    {
        if(flashOnHit!=null)
            StopCoroutine(flashOnHit);

        flashOnHit = StartCoroutine(MuzzleFlashRoutine());
    }
    public IEnumerator MuzzleFlashRoutine()
    {
        muzzzleFlash.enabled = true;
        yield return flashWait;
        muzzzleFlash.enabled = false;
        flashOnHit = null;
    }
}
