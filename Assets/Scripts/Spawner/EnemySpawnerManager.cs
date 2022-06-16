using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerManager : MonoBehaviour
{

    public List<Wave> waves = new List<Wave>();
    [SerializeField]
    public int waveCount;
    [SerializeField] private float maxTimeBetweenWaves = 2f;
    [SerializeField] private float waveCountdown;
    public List<Enemy> currentEnemiesInWave = new List<Enemy>();

    public enum WaveState
    {
        Spawning,
        Waiting
    };

    public WaveState waveState;
    [SerializeField] private Transform[] enemySpawnPoints;
    public bool isAllEnemiesSpawned;
    private List<Transform> _possibleEnemySpawnPoints = new List<Transform>();
    [Header("NEXT WAVE PARAMETERS")] [SerializeField]
    private int maxEnemiesInWave = 6;
    [SerializeField] private float minEnemySpawnRateTimer = 0.5f;
    [SerializeField] private int numberOfEnemiesToAddNextWave = 2;
    [SerializeField] private float reducingWaveTimeRate = 0.1f;
    
    private static EnemySpawnerManager _enemySpawnerManager;
    public static EnemySpawnerManager Instance => _enemySpawnerManager;
    
    [Header("NUMBER DIFFICULTIES")] [Space(20)]
    public NumberDifficulty[] numberDifficulties;
    
    [Serializable]
    public struct NumberDifficulty
    {
        public List<ScriptableObjectScore> scriptableObjectScores;
    }
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
            return;

        if (waveState == WaveState.Spawning) 
            waveCountdown -= Time.deltaTime;

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
        if (GameManager.Instance != null) GameManager.Instance.textWave.text = $"Wave {waveCount + 1}";
        _possibleEnemySpawnPoints.AddRange(enemySpawnPoints);
        for (var i = 0; i < wave.numberOfEnemyWave; i++)
        {
            SpawnEnemy(wave);
            yield return waitForSecondsEnemySpawnRate;
        }
        
    }

    private void SpawnEnemy(Wave wave)
    {
        //Pick a random spawn point 
        var randomSpawnTransform = _possibleEnemySpawnPoints[Random.Range(0, _possibleEnemySpawnPoints.Count)];
        var enemy = Instantiate(wave.enemies[Random.Range(0, wave.enemies.Length)], randomSpawnTransform.position, Quaternion.identity);
        currentEnemiesInWave.Add(enemy);
        _possibleEnemySpawnPoints.Remove(randomSpawnTransform);
        if (_possibleEnemySpawnPoints.Count <= 0)
        {
            _possibleEnemySpawnPoints.AddRange(enemySpawnPoints);
        }
        
        if (currentEnemiesInWave.Count == wave.numberOfEnemyWave)
        {
            ScoreManager.Instance.MechanicLaunched();
            isAllEnemiesSpawned = true;
            _possibleEnemySpawnPoints.Clear();
        }

    }

    public void CheckAllEnemiesWave(Enemy enemy)
    {
        currentEnemiesInWave.Remove(enemy);
        if (currentEnemiesInWave.Count <= 0)
        {
            isAllEnemiesSpawned = false;
            if (!GameManager.Instance.isGameOver)
            {
                StartNewWave();
            }
        }
    }

    private void StartNewWave()
    {
        var lastWave = waves[waveCount];
        
        var newNumberOfEnemyWave = lastWave.numberOfEnemyWave + numberOfEnemiesToAddNextWave;
        var newEnemySpawnRate = lastWave.enemySpawnRate - reducingWaveTimeRate;
        
        var numberOfEnemyWave = newNumberOfEnemyWave <= maxEnemiesInWave ? newNumberOfEnemyWave : lastWave.numberOfEnemyWave;
        var enemySpawnRate = newEnemySpawnRate >= minEnemySpawnRateTimer ? newEnemySpawnRate : lastWave.enemySpawnRate;
        
        waveCount++;
        var newWave = new Wave($"Wave {waveCount}", numberOfEnemyWave, enemySpawnRate, lastWave.enemies);
        
        waves.Add(newWave);
        
        waveState = WaveState.Spawning;

        if (newWave.numberOfEnemyWave >= maxEnemiesInWave)
        {
            ChangeRandomNumber();   
        }
    }

    void ChangeRandomNumber()
    {
        var scoreObjects = ScoreManager.Instance.scoreObjects;
        var randomNumber = Random.Range(0, scoreObjects.Count);
        
        RemoveAndChangeNumber(randomNumber, scoreObjects[randomNumber]);
    }

    void RemoveAndChangeNumber(int number, ScriptableObjectScore scriptableObjectScore)
    {
        var scoreObjects = ScoreManager.Instance.scoreObjects;
        switch (scriptableObjectScore.difficulty)
        {
            case ScriptableObjectScore.Difficulty.Easy : 
                scoreObjects.RemoveAt(number);
                scoreObjects.Insert(number, numberDifficulties[0].scriptableObjectScores[number]);
                break;
            case ScriptableObjectScore.Difficulty.Medium : 
                scoreObjects.RemoveAt(number);
                scoreObjects.Insert(number, numberDifficulties[1].scriptableObjectScores[number]);
                break;
            case ScriptableObjectScore.Difficulty.Hard : 
                scoreObjects.RemoveAt(number);
                scoreObjects.Insert(number, numberDifficulties[2].scriptableObjectScores[number]);
                break;
            case ScriptableObjectScore.Difficulty.Extreme :
                ChangeRandomNumber();
                break;
        }
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
