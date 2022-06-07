using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    public List<Wave> waves = new List<Wave>();

    [SerializeField] private float maxTimeBetweenWaves = 2f;
    [SerializeField] private float waveCountdown;
    void Start()
    {
        waveCountdown = maxTimeBetweenWaves;
    }
    
    void Update()
    {
        
    }
    
        
    [Serializable]
    public class Wave
    {
        public string waveName;
        public int numberOfEnemyWave;
        public float waveRate;

        public Wave(string waveName, int numberOfEnemyWave, float waveRate)
        {
            this.waveName = waveName;
            this.numberOfEnemyWave = numberOfEnemyWave;
            this.waveRate = waveRate;
        }
        
    }
}
