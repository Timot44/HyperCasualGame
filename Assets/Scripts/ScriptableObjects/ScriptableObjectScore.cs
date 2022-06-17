using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object / Score")]
public class ScriptableObjectScore : ScriptableObject
{
    public int numberEnemy;

    public List<string> possibilityOfStrings;
    public Difficulty difficulty;
    
    
    public enum Difficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
        Extreme = 3
    }
}
