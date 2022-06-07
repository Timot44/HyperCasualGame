using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public ScoreManager scoreManager;

	private Camera _cam;

	private void Start()
	{
		_cam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
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
}