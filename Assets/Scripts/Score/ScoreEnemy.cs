using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEnemy : MonoBehaviour
{
    public int numberOfEnemy;
    public TextMeshPro textOnEnemy;

    [HideInInspector] public int spawnInListMin, spawnInListMax;

    private void Start()
    {
	    switch (numberOfEnemy)
	    {
		    case 0 : spawnInListMin = 0; spawnInListMax = 4; break;
		    case 1 : spawnInListMin = 0; spawnInListMax = 5; break;
		    case 2 : spawnInListMin = 0; spawnInListMax = 6; break;
		    case 3 : spawnInListMin = 0; spawnInListMax = 7; break;
		    case 4 : spawnInListMin = 0; spawnInListMax = 8; break;
		    case 5 : spawnInListMin = 2; spawnInListMax = 8; break;
		    case 6 : spawnInListMin = 3; spawnInListMax = 8; break;
		    case 7 : spawnInListMin = 3; spawnInListMax = 8; break;
	    }
    }
}
