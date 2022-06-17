using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEnemy : MonoBehaviour
{
    public int numberOfEnemy;
    public TextMeshPro textOnEnemy;

    [HideInInspector] public int spawnInListRemove;

    private void Start()
    {
	    switch (numberOfEnemy)
	    {
		    case 0 : spawnInListRemove = 3; break;
		    case 1 : spawnInListRemove = 2; break;
		    case 2 : spawnInListRemove = 1; break;
		    case 3 : spawnInListRemove = 0; break;
		    case 4 : spawnInListRemove = 0; break;
		    case 5 : spawnInListRemove = 0; break;
		    case 6 : spawnInListRemove = 0; break;
		    case 7 : spawnInListRemove = 0; break;
	    }
    }
}
