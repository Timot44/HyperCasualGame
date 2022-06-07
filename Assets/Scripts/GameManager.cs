using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public ScoreManager scoreManager;
	public GameObject trailFinger;
	
	public float distanceFromCamera = 5;
	private Camera _cam;

	private void Start()
	{
		_cam = Camera.main;
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
			Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit))
			{
				if (hit.collider.name == scoreManager.enemiesInGoodOrder[0].name)
				{
					hit.collider.TryGetComponent(out ScoreEnemy scoreEnemy);
					
					scoreManager.enemiesInGoodOrder.Remove(scoreEnemy);
					
					hit.collider.gameObject.SetActive(false);
				}
			}
		}
	}
	
	void MoveTrailToCursor(Vector3 screenPosition)
	{
		trailFinger.transform.position = _cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
	}
}