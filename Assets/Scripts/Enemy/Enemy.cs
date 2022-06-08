using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public EnemyHealth enemyHealth;
  
    [Header("MOVEMENT PARAMETERS")]
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rangeMovement = 1f;
    
    private PlayerHealth _playerHealth;
    private string _playerTag = "Player";
    protected virtual void Awake()
    {
        enemyHealth = new EnemyHealth();
    }

    protected virtual void Start()
   {
       target = GameObject.FindWithTag("Player").transform;
      _playerHealth = target.gameObject.GetComponent<PlayerHealth>();
       enemyHealth.EnemyDeathEvent += OnEnemyDeathEvent;
   }

  

   protected virtual void Update()
   {
        MoveToPlayer();
        RotateTowardPlayer();
   }

   protected virtual void MoveToPlayer()
   {
       if (Vector3.Distance(transform.position, target.position) > rangeMovement)
       {
           transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
       }
       
   }

   protected virtual void RotateTowardPlayer()
   {
       var lookForward = target.position - transform.position;
       transform.rotation = Quaternion.LookRotation(lookForward, Vector3.up);
   }

   protected virtual void OnEnemyDeathEvent()
   {
       //TODO VFX explosion enemy
       Destroy(gameObject);
       if (EnemySpawnerManager.Instance != null) EnemySpawnerManager.Instance.CheckAllEnemiesWave(this);
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.CompareTag(_playerTag))
       {
           _playerHealth.TakeDamage(1);
           enemyHealth.TakeDamage(enemyHealth.maxHealth);
       }
   }
}
