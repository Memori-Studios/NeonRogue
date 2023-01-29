using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public float minDistanceFromPlayerToSpawn, maxDistanceFromPlayerToSpawn;
    public Wave[] waves;
    int enemiesRemainingToSpawn, enemiesRemainingAlive, currentWaveNumber = 0;
    float nextSpawnTime;
    private int spawnLayer = 10;
    private int spawnLayerMask;
    public List<Collider> spawnZones;
    public SpawnBlocker spawnBlocker;
    public bool spawnClose;
    Wave currentWave;
    Enemy enemyToSpawn;
    private void Start()
    {
        if(spawnClose)
            spawnBlocker.transform.localScale = new Vector3(5, 1, 5);
            
        spawnLayerMask = (1 << spawnLayer);
        float totalGameTime = 0;
        foreach(Wave wave in waves)
        {
            wave.CreateWave();
            totalGameTime+= wave.timeBetweenSpawns * wave.enemies.Count;
        }
        Debug.Log($"Total Game Time: {FormatTime(totalGameTime)}");
    }
    public string FormatTime( float time )
    {
        int minutes = (int) time / 60 ;
        int seconds = (int) time - 60 * minutes;
        int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
    }
    public void Update(){

        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            enemyToSpawn = currentWave.enemies[enemiesRemainingToSpawn];

            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            if(spawnZones.Count==0)
                return;

            Vector3 spawnPoint = RandomPointInBounds(spawnZones[Random.Range(0,spawnZones.Count)].bounds);

            Enemy spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity) as Enemy;
            // spawnedEnemy.OnDeath += OnEnemyDeath;

            if(enemiesRemainingToSpawn==0)
                NextWave();
        }
    }
    public void RespawnEnemy(Enemy spawnedEnemy)
    {
        if(spawnZones.Count==0)
        {
            Debug.Log($"No spawn zones found for {spawnedEnemy.name}");
            return;
        }

        Vector3 spawnPoint = RandomPointInBounds(spawnZones[Random.Range(0,spawnZones.Count)].bounds);

        spawnedEnemy.gameObject.transform.position = spawnPoint;
        // spawnedEnemy.OnDeath += OnEnemyDeath;
    }
    // private void OnEnemyDeath()
    // {
    //     enemiesRemainingAlive--;
    //     if(enemiesRemainingAlive==0)
    //     {
    //         NextWave();
    //     }
    // }
    public void NextWave()
    {
        currentWaveNumber ++;
        if(currentWaveNumber -1 >= waves.Length)
            return;

        currentWave = waves[currentWaveNumber - 1];

        currentWave.CreateWave();
        enemiesRemainingToSpawn = currentWave.enemies.Count;
        enemiesRemainingAlive = enemiesRemainingToSpawn;
    }
    [System.Serializable] public class Wave{
        public float timeBetweenSpawns;
        public List<SubWave> subWaves;
        [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
        public void CreateWave()
        {
            foreach(SubWave subWave in subWaves)
            {
                for(int i = 0; i < subWave.enemyCount; i++)
                    enemies.Add(subWave.enemy);
            }
            ListExtensions.Shuffle(enemies);
        }
    }
    [System.Serializable] public class SubWave{
        public int enemyCount;
        public Enemy enemy;
    }
    
    public static Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        0,
        // Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
}

}
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list) 
    {
        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }
    public static void Swap<T>(this IList<T> list, int i, int j) 
    {
        (list[i], list[j]) = (list[j], list[i]);
    }
}
