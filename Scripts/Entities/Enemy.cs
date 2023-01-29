using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TJAudio;

public class Enemy : LivingEntity, IDamageable
{
    [SerializeField] private ExpShard expShard;
    public float moveSpeed, attackDistanceThreshold = 1f, timeBetweenAttacks = 1f;
    float nextAttackTime, attackRange;
    bool hasTarget;
    public float damage;
    public SkinnedMeshRenderer meshRenderer;
    public float flashDuration;
    private Coroutine flashOnHit;
    private WaitForSeconds flashWait;
    private Material origionalMaterial;
    Transform targetTransform;
    LivingEntity player;
    [SerializeField] protected Animator animator;
    Rigidbody rb;

    protected bool enemyMovementOverride;

    protected override void Start()
    {
        base.Start();
        if(PlayerManager.instance==null)
            return;

        targetTransform = PlayerManager.instance.gameObject.transform;
        player = PlayerManager.instance.GetComponent<LivingEntity>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        flashWait = new WaitForSeconds(flashDuration);
        origionalMaterial = meshRenderer.material;

        GetComponent<LivingEntity>().OnDamage += EnemyTakeDamage;
        player.OnDeath += OnTargetDeath;
        this.OnDeath += OnEnemyDeath;
        hasTarget = true;
    }
    private void Update()
    {
        if(dead)
            return;

        if(targetTransform == null)
            DestroyImmediate(gameObject);

        if(enemyMovementOverride)
            return;

        if(TooFarAway())
        {
            PlayerManager.instance.spawner.RespawnEnemy(this);
        }

        if(rb==null)
            return;
            
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        animator.speed = Sandevistan.instance.sandevistanActive ? 1 / 20 : 1;


        float movementSpeed = Sandevistan.instance.sandevistanActive ? moveSpeed / 20 : moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * movementSpeed);
        
        if(Sandevistan.instance.sandevistanActive)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
        
        transform.LookAt(targetTransform.position);

        if(Time.time > nextAttackTime)
        {
            float sqrDistance = (targetTransform.position - transform.position).sqrMagnitude;
            // Debug.Log($"tried to attack {sqrDistance}");
            if(sqrDistance < Mathf.Pow(attackDistanceThreshold, 2)){
                nextAttackTime = Time.time + timeBetweenAttacks;
                Attack();
            }
        }
    }
    private bool TooFarAway()
    {
        if(targetTransform == null)
            return false;

        float distanceAway = Vector3.Distance(targetTransform.position, transform.position);
        // if((targetTransform.position - transform.position).sqrMagnitude > Mathf.Pow(attackDistanceThreshold, 2))
        //     return true;
        if(distanceAway > 30)
        {
            Debug.Log($"too far away {distanceAway}");
            return true;
        }

        return false;
    }
    private void OnTargetDeath()
    {
        hasTarget = false;
    }
    private void Attack(){
        player.TakeDamage(damage);
        IAudioRequester.instance.PlaySFX("playerDamage");
    }
    private void OnEnemyDeath()
    {
        StartCoroutine(DieAfterFlash());
        PlayerManager.instance.enemiesSlain++;
    }
    public IEnumerator DieAfterFlash()
    {
        while(flashOnHit!=null)
            yield return null;

        Instantiate(expShard.shardPrefab, this.transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
    public IEnumerator FlashOnHit()
    {
        meshRenderer.material = PlayerManager.instance.flashMaterial;
        yield return flashWait;
        meshRenderer.material = origionalMaterial;
        flashOnHit = null;
    }
    public void Flash()
    {
        if(flashOnHit!=null)
            StopCoroutine(flashOnHit);

        flashOnHit = StartCoroutine(FlashOnHit());
    }
    public void EnemyTakeDamage(float damage)
    {
        health -= damage;
        if(!dead)
            Flash();
        
        if(health<=0&&!dead)
            Die();
    }
    
}
