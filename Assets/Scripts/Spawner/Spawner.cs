using System;
using System.Collections;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}
public class Spawner : MonoBehaviour
{
    public static Action OnWaveComplete;

    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")]
    [SerializeField] private ObjectPooler enemyWave10Pooler;
    [SerializeField] private ObjectPooler enemyWave11To20Pooler;
    [SerializeField] private ObjectPooler enemyWave21To30Pooler;
    [SerializeField] private ObjectPooler enemyWave31To40Pooler;
    [SerializeField] private ObjectPooler enemyWave41To50Pooler;
 

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;

    private Waypoint _waypoint;

    private void Start()
    {
        _waypoint = GetComponent<Waypoint>();
        _enemiesRamaining = enemyCount;
    }
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }
    private float GetSpawnDelay()
    {
        float delay = 0;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }    
    private float GetRandomDelay()
    {
        float randomTimer = UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }
    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
       
        if (currentWave <= 10)
        {
            return enemyWave10Pooler;
        }

        if (currentWave > 10 && currentWave <= 20)
        {
            return enemyWave11To20Pooler;
        }

        if (currentWave > 20 && currentWave <= 30)
        {
            return enemyWave21To30Pooler;
        }

        if (currentWave > 30 && currentWave <= 40)
        {
            return enemyWave31To40Pooler;
        }

        if (currentWave > 40)
        {
            return enemyWave41To50Pooler;
        }

        return null;
    }
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0;
        _enemiesSpawned = 0;
    }
    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0)
        {
            OnWaveComplete?.Invoke();
            StartCoroutine(NextWave());
        }
    }
    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }
    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }
    
}
