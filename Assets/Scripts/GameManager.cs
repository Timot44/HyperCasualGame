using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("TRAIL SETTINGS")]
	public GameObject trailFinger;
	public float distanceFromCamera = 5;
	
	[Header("SCORE")]
	public ScoreManager scoreManager;
	public int score;
	public int bonus;
	public TextMeshPro textScore;
	
	private Camera cam;

	private void Start()
	{
		cam = Camera.main;
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

			if (Physics.Raycast(ray, out var hit))
			{
				if (hit.collider.name == scoreManager.enemiesInGoodOrder[0].name)
				{
					hit.collider.TryGetComponent(out ScoreEnemy scoreEnemy);
					
					scoreManager.enemiesInGoodOrder.Remove(scoreEnemy);
					
					hit.collider.gameObject.SetActive(false);

					score++;
					textScore.text = $"Score : {score}";
				}
				else
				{
					Debug.LogWarning("Wrong Enemy");
				}
			}
		}
	}
	
	void MoveTrailToCursor(Vector3 screenPosition)
	{
		trailFinger.transform.position = cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
	}
}