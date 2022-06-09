using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	public int currentHealth;
	public int maxHealth;

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
		playerPulsingParticle.gameObject.SetActive(false);
		if (GameManager.Instance != null)
		{
			GameManager.Instance.SetUpGameOver();
		}
	}
}
