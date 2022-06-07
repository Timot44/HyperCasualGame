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
    [SerializeField]
    private bool isStartNextWave = true;

    [SerializeField] private Transform[] enemySpawnPoints;
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
        if (isStartNextWave)
        {
            waveCountdown -= Time.deltaTime;
        }

        if (waveCountdown <= 0 && isStartNextWave)
        {
            StartCoroutine(SpawnWave(waves[waveCount]));
            isStartNextWave = false;
            waveCountdown = maxTimeBetweenWaves;
        }
    }

    IEnumerator SpawnWave(Wave wave)
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
        Instantiate(wave.enemies[Random.Range(0, wave.enemies.Length)], randomSpawnTransform.position, Quaternion.identity);
    }


    [Serializable]
    public class Wave
    {
        public string waveName;
        public int numberOfEnemyWave;
        public float enemySpawnRate;
        public Enemy[] enemies;
        public Wave(string waveName, int numberOfEnemyWave, float enemySpawnRate)
        {
            this.waveName = waveName;
            this.numberOfEnemyWave = numberOfEnemyWave;
            this.enemySpawnRate = enemySpawnRate;
        }
        
    }
}
