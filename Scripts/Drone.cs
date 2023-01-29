using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Enemy target;
    public float rotationSpeed = 10f, droneRateOfFire = 0.5f, nextShotTime = 0f, damage = 10f, 
    moveSpeed = 5f, projectileVariance = 5f, projectilePiercing = 1f, muzzleVelocity = 100f, flashWait = 0.02f;
    public Projectile projectilePrefab;
    public Transform droneFireSpot, drone;
    public Light droneMuzzleFlash;
    private Coroutine flashOnHit;
    Quaternion lookRotation;
    void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        FaceNearestEnemy();
        Fire();
    }
    private void FixedUpdate()
    {
        MoveToPlayer();
    }
    void FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float minDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy;
            }
        }
    }
    void FaceNearestEnemy()
    {
        Vector3 direction = (target.transform.position - drone.position).normalized;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        drone.rotation = Quaternion.Slerp(drone.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    void MoveToPlayer()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, PlayerManager.instance.transform.position, Time.deltaTime * moveSpeed);
    }
    void Fire()
    {
        if(Time.time > nextShotTime)
        {
            nextShotTime = Time.time + (droneRateOfFire / PlayerManager.instance.playerStats.RateOfFire);

            MuzzleFlash();
                
            float offset = Random.Range(-projectileVariance, projectileVariance);
            Projectile newProjectile = Instantiate(projectilePrefab, droneFireSpot.position, lookRotation) as Projectile;

            newProjectile.SetStats(muzzleVelocity, damage, (int)projectilePiercing, 0, 1);
        }
    }
    public IEnumerator FlashOnHit()
    {
        droneMuzzleFlash.enabled = true;
        yield return flashWait;
        droneMuzzleFlash.enabled = false;
        flashOnHit = null;
    }
    public void MuzzleFlash()
    {
        if(flashOnHit!=null)
            StopCoroutine(flashOnHit);

        flashOnHit = StartCoroutine(FlashOnHit());
    }
}
