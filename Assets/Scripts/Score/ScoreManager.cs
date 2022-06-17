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

		var scoreObjectNewList = new List<ScriptableObjectScore>(scoreObjects);

		for (int i = maxEnemyNumber - enemies.Count - 1; i >= 0; i--)
		{
			var maxValue = Mathf.Max(scoreObjectNewList.Count);
			scoreObjectNewList.RemoveAt(maxValue - 1);
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			var numberList = scoreObjectNewList.Count;
			var rSo = Random.Range(0, numberList);

			enemies[i].numberOfEnemy = scoreObjectNewList[rSo].numberEnemy;

			var rTMP = Random.Range(0, scoreObjectNewList[rSo].possibilityOfStrings.Count);
			enemies[i].textOnEnemy.text = scoreObjectNewList[rSo].possibilityOfStrings[rTMP];

			enemiesInGoodOrder.Add(enemies[i]);

			scoreObjectNewList.RemoveAt(rSo);
		}

		enemiesInGoodOrder = enemiesInGoodOrder.OrderBy(ch => ch.numberOfEnemy).ToList(); // Put the enemy in the right order
	}
}