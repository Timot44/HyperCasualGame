using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<ScriptableObjectScore> scoreObjects;

    public List<ScoreEnemy> enemies;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            var newList = new List<ScriptableObjectScore>(scoreObjects);
            
            for (int i = maxEnemyNumber - enemies.Count - 1; i >= 0; i--)
            {
                var maxValue = Mathf.Max(newList.Count);
                newList.RemoveAt(maxValue-1);
            }
            
            for (int i = 0; i < enemies.Count; i++)
            {
                var numberList = newList.Count;
                var rSo = Random.Range(0, numberList);

                enemies[i].numberOfEnemy = newList[rSo].numberEnemy;

                var rTMP = Random.Range(0, newList[rSo].possibilityOfStrings.Count);
                enemies[i].textOnEnemy.text = newList[rSo].possibilityOfStrings[rTMP];

                newList.RemoveAt(rSo);
            }
        }
    }
}
