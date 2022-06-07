using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    public List<Wave> waves = new List<Wave>();
    [SerializeField]
    private int waveCount = 0;
    [SerializeField] private float maxTimeBetweenWaves = 2f;
    [SerializeField] private float waveCountdown;
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
        throw new NotImplementedException();
    }


    [Serializable]
    public class Wave
    {
        public string waveName;
        public int numberOfEnemyWave;
        public float waveRate;
        public GameObject[] enemies;
        public Wave(string waveName, int numberOfEnemyWave, float waveRate)
        {
            this.waveName = waveName;
            this.numberOfEnemyWave = numberOfEnemyWave;
            this.waveRate = waveRate;
        }
        
    }
}
