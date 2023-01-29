using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBoss : Enemy
{
    [Header("MechBoss Specific")]
    [SerializeField] private float muzzleVelocity = 2f;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float range = 10f;
    [SerializeField] private Transform projectileSpawnPoint;

    [Header("Weapon")]
    private Coroutine flashOnHit;
    private WaitForSeconds flashWait;
    [SerializeField] private Light muzzzleFlash;
    [SerializeField] private float rateOfFire = 0.2f, roundsToFire = 10f;
    bool lookAtPlayer = false;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Fire", 5f, 10f);
    }

    private void Fire()
    {
        StartCoroutine(FirePause());
    }
    private IEnumerator FirePause()
    {
        enemyMovementOverride = true;
        float amountToFire = roundsToFire;
        animator.SetBool("isFiring", true);
        lookAtPlayer = true;
        StartCoroutine(LookAtPlayer());

        yield return new WaitForSeconds(1f);

        while(amountToFire>0)
        {
            MuzzleFlash();
            projectileSpawnPoint.LookAt(new Vector3(PlayerManager.instance.transform.position.x, 1f, PlayerManager.instance.transform.position.z));
            Quaternion newrotation = projectileSpawnPoint.rotation;
            Projectile newProjectile = Instantiate(projectile, projectileSpawnPoint.position, newrotation) as Projectile;
            amountToFire--;
            newProjectile.SetStats(muzzleVelocity, damage, 1, 0, 1);
            yield return new WaitForSeconds(rateOfFire);
        }

        enemyMovementOverride = false;
        animator.SetBool("isFiring", false);
        lookAtPlayer = false;
    }
    private IEnumerator LookAtPlayer()
    {
        while(lookAtPlayer)
        {
            Vector3 lookPos = new Vector3(PlayerManager.instance.transform.position.x, this.transform.position.y, PlayerManager.instance.transform.position.z);
            this.transform.LookAt(lookPos);
            yield return null;
        }
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
