using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
	public List<ScriptableObjectScore> scoreObjects;
	public List<ScoreEnemy> enemies;
	public List<ScoreEnemy> enemiesInGoodOrder;
	
	public int maxEnemyNumber;

	#region Singleton

	private static ScoreManager scoreManager;

	public static ScoreManager Instance => scoreManager;
	// Start is called before the first frame update

	private void Awake()
	{
		scoreManager = this;
	}

	#endregion

	// Update is called once per frame
	public void MechanicLaunched()
	{
		enemies.Clear();
		enemiesInGoodOrder.Clear();
		
		var enemySpawnerManager = EnemySpawnerManager.Instance;
		
		foreach (var e in enemySpawnerManager.currentEnemiesInWave)
		{
			if (e.TryGetComponent(out ScoreEnemy scoreEnemy))
			{
				enemies.Add(scoreEnemy);
			}
		}

		var soListNumber = new List<ScriptableObjectScore>(scoreObjects);
			
		for (int i = maxEnemyNumber - enemies.Count - 1; i >= 0; i--)
		{
			soListNumber.RemoveAt(soListNumber.Count - 1);
		}

		var rSo = 0;
		for (int i = 0; i < enemies.Count; i++)
		{
			while (rSo > enemies.Count)
			{
				rSo = Random.Range(enemies[i].spawnInListMin, enemies[i].spawnInListMax);// random number with exceptions
			}
			
			enemies[i].numberOfEnemy = soListNumber[rSo].numberEnemy; // numberEnemy is equal to so[rSo]

			var rTMP = Random.Range(0, soListNumber[rSo].possibilityOfStrings.Count); // rTMP to display on top of enemy
			enemies[i].textOnEnemy.text = soListNumber[rSo].possibilityOfStrings[rTMP];

			enemiesInGoodOrder.Add(enemies[i]);

			soListNumber.RemoveAt(rSo);
		}

		enemiesInGoodOrder = enemiesInGoodOrder.OrderBy(ch => ch.numberOfEnemy).ToList(); // Put the enemy in the right order
	}
}