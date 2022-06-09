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
	void Start()
	{
		var newList = new List<ScriptableObjectScore>(scoreObjects);

		for (int i = maxEnemyNumber - enemies.Count - 1; i >= 0; i--)
		{
			var maxValue = Mathf.Max(newList.Count);
			newList.RemoveAt(maxValue - 1);
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			var numberList = newList.Count;
			var rSo = Random.Range(0, numberList);

			enemies[i].numberOfEnemy = newList[rSo].numberEnemy;

			var rTMP = Random.Range(0, newList[rSo].possibilityOfStrings.Count);
			enemies[i].textOnEnemy.text = newList[rSo].possibilityOfStrings[rTMP];

			enemiesInGoodOrder.Add(enemies[i]);

			newList.RemoveAt(rSo);
		}

		enemiesInGoodOrder = enemiesInGoodOrder.OrderBy(ch => ch.numberOfEnemy).ToList();
	}
}