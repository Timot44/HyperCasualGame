using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEnemy : MonoBehaviour
{
    public int numberOfEnemy;
    public TextMeshPro textOnEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        textOnEnemy = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
