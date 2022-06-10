using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	public int currentHealth;
	public int maxHealth;
	public TextMeshProUGUI textScore, textHighScore;

	public Transform parentInstantiate;
	public GameObject currentMeshPlayer;
	public List<GameObject> gameObjectToReplace;

	[SerializeField] private ParticleSystem playerPulsingParticle;
	public void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= (int) damage;
		
		currentMeshPlayer.SetActive(false);
		currentMeshPlayer = Instantiate(gameObjectToReplace[0], transform.position, Quaternion.identity, parentInstantiate);
		
		gameObjectToReplace.RemoveAt(0);
		
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		var score = GameManager.Instance.score;
		
		textScore.text = $"Score : {Mathf.Ceil(score)}";

		if (!PlayerPrefs.HasKey("HighScore"))
		{
			PlayerPrefs.SetFloat("HighScore", Mathf.Ceil(score));
			textHighScore.text = $"HighScore : {Mathf.Ceil(score)}";
		}
		else
		{
			if (PlayerPrefs.GetFloat("HighScore") < score)
			{
				textHighScore.text = $"HighScore : {Mathf.Ceil(score)}";
			}
			else
			{
				textHighScore.text = $"HighScore : {PlayerPrefs.GetFloat("HighScore")}";
			}
		}

		playerPulsingParticle.gameObject.SetActive(false);
		if (GameManager.Instance != null)
		{
			GameManager.Instance.SetUpGameOver();
		}
	}
}
