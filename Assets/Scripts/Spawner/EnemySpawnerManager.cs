using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerManager : MonoBehaviour
{

    public List<Wave> waves = new List<Wave>();
    [SerializeField]
    private int waveCount = 0;
    [SerializeField] private float maxTimeBetweenWaves = 2f;
    [SerializeField] private float waveCountdown;
    public List<Enemy> currentEnemiesInWave = new List<Enemy>();
    private enum WaveState
    {
        Spawning,
        Waiting
    };
    [SerializeField]
    private WaveState waveState;
    
    [SerializeField] private Transform[] enemySpawnPoints;
    public bool isAllEnemiesSpawned;
    [SerializeField] private int numberOfEnemiesToAddNextWave = 2;
    [SerializeField] private float reducingWaveTimeRate = 0.1f;
    private static EnemySpawnerManager _enemySpawnerManager;
    public static EnemySpawnerManager Instance => _enemySpawnerManager;
    private void Awake()
    {
        _enemySpawnerManager = this;
    }

    private void Start()
    {
        waveCountdown = maxTimeBetweenWaves;
    }
    
   private void Update()
    {
        GenerateWave();
    }

    private void GenerateWave()
    {
        if (waveState == WaveState.Waiting)
        {
            return;
        }

        if (waveState == WaveState.Spawning)
        {
            waveCountdown -= Time.deltaTime;
        }

        if (waveCountdown <= 0 && waveState == WaveState.Spawning)
        {
            StartCoroutine(SpawnWave(waves[waveCount]));
            waveState = WaveState.Waiting;
            waveCountdown = maxTimeBetweenWaves;
        }
    }

   private IEnumerator SpawnWave(Wave wave)
    {
        var waitForSecondsEnemySpawnRate = new WaitForSeconds(wave.enemySpawnRate);
     
        for (var i = 0; i < wave.numberOfEnemyWave; i++)
        {
            SpawnEnemy(wave);
            yield return waitForSecondsEnemySpawnRate;
        }
        
    }

    private void SpawnEnemy(Wave wave)
    {
        //Pick a random spawn point 
        var randomSpawnTransform = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
        var enemy = Instantiate(wave.enemies[Random.Range(0, wave.enemies.Length)], randomSpawnTransform.position, Quaternion.identity);
        currentEnemiesInWave.Add(enemy);
        if (currentEnemiesInWave.Count == wave.numberOfEnemyWave) isAllEnemiesSpawned = true;

    }

    public void CheckAllEnemiesWave(Enemy enemy)
    {
        currentEnemiesInWave.Remove(enemy);
        if (currentEnemiesInWave.Count <= 0)
        {
            StartNewWave();
        }
        
    }

    private void StartNewWave()
    {
        var lastWave = waves[waveCount];
        var newWave = new Wave($"Wave {waveCount + 1}", lastWave.numberOfEnemyWave + numberOfEnemiesToAddNextWave,
            lastWave.enemySpawnRate - reducingWaveTimeRate, lastWave.enemies);
        waves.Add(newWave);
        waveCount++;
        waveState = WaveState.Spawning;
    }

    [Serializable]
    public class Wave
    {
        public string waveName;
        public int numberOfEnemyWave;
        public float enemySpawnRate;
        public Enemy[] enemies;
        public Wave(string waveName, int numberOfEnemyWave, float enemySpawnRate, Enemy[] enemies)
        {
            this.waveName = waveName;
            this.numberOfEnemyWave = numberOfEnemyWave;
            this.enemySpawnRate = enemySpawnRate;
            this.enemies = enemies;
        }
        
    }
}
