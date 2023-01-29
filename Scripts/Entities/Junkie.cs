using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junkie : Enemy
{
    [Header("Junkie Specific")]
    [SerializeField] private float range = 2f;
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private float explosionRadius = 2f;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("CheckForPlayer", 1f, 1f);
    }

    private void CheckForPlayer()
    {
        float distanceAway = Vector3.Distance(transform.position, PlayerManager.instance.transform.position);

        if(distanceAway < range)
        {
            StartCoroutine(Explode());
        }
    }
    private IEnumerator Explode()
    {
        InvokeRepeating("Flash", 0f, flashDuration*1.5f);
        enemyMovementOverride = true;
        animator.Play("WitnessMe");

        yield return new WaitForSeconds(0.5f);
        Explosion explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity, null) as Explosion;
        Destroy(explosion.gameObject, 3f);
        Destroy(gameObject);
    }
}
