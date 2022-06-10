using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
	[Header("TOUCH SETTINGS")]
	public GameObject trailFinger;
	public float distanceFromCamera = 5;
	public LayerMask layerEnemy;
	
	[Header("SCORE")]
	public ScoreManager scoreManager;
	public float score;
	public float multiplierToAdd = 0.1f;

	public TextMeshPro textScore;
	
	private Camera cam;
	private static GameManager _gameManager;
	public static GameManager Instance => _gameManager;
	[Header("GAME ENDING PARAMETERS")] [SerializeField]
	private GameObject panelGameOver;
	[SerializeField] private Volume postProcess;
	private DepthOfField _depthOfField;
	public bool isGameOver;
	private void Awake()
	{
		_gameManager = this;
		postProcess.profile.Reset();
	}

	private void Start()
	{
		cam = Camera.main;
		if (postProcess.profile.TryGet(out DepthOfField depthOfField)) _depthOfField = depthOfField;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButton(0))
		{
			trailFinger.SetActive(true);
			MoveTrailToCursor(Input.mousePosition);
		}
		else
		{
			trailFinger.SetActive(false);
			MoveTrailToCursor(Input.mousePosition);
		}
		
		if (Input.GetMouseButton(0) && scoreManager.enemiesInGoodOrder.Count >0)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerEnemy))
			{
				if (hit.collider.name == scoreManager.enemiesInGoodOrder[0].name)
				{
					hit.collider.TryGetComponent(out ScoreEnemy scoreEnemy);
					hit.collider.TryGetComponent(out Enemy enemy);
					
					scoreManager.enemiesInGoodOrder.Remove(scoreEnemy);
					scoreManager.enemies.Remove(scoreEnemy);
					
					enemy.enemyHealth.TakeDamage(1);
					
					hit.collider.gameObject.SetActive(false);

					multiplierToAdd += 0.01f;
					score++;
					
					var addMultiplier = score * multiplierToAdd;
					Debug.Log(addMultiplier);
					
					score += addMultiplier;

					var scoreCap = Mathf.Ceil(score);
					
					textScore.text = $"Score : {scoreCap}";
				}
				else
				{
					multiplierToAdd = 0.1f;
				}
			}
		}
	}
	
	void MoveTrailToCursor(Vector3 screenPosition)
	{
		trailFinger.transform.position = cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
	}

	public void SetUpGameOver()
	{
		_depthOfField.active = true;
		panelGameOver.SetActive(true);
		isGameOver = true;
	}
}