using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyHealth: IDamageable
{
   
    public int maxHealth = 1;
    private int _health;
    public event Action EnemyDeathEvent;

    public EnemyHealth()
    {
        _health = maxHealth;
    }
    

    public void TakeDamage(float damage)
    {
        _health -= (int)damage;
        if (_health <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        if (EnemyDeathEvent != null) EnemyDeathEvent();
    }
 
}
