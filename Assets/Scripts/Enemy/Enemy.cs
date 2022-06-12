using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    [Header("VFX")] [SerializeField] protected ParticleSystem vfxDeath;
    public ParticleSystem vfxWrongParticle;

    [Header("MOVEMENT PARAMETERS")] [SerializeField]
    private Transform target;

    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] private float rangeMovement = 1f;
    [SerializeField] private Vector3 targetOffset = Vector3.up;
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
            transform.position = Vector3.MoveTowards(transform.position, target.position + targetOffset,
                moveSpeed * Time.deltaTime);
        }
    }

    protected virtual void RotateTowardPlayer()
    {
        var lookForward = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(lookForward + targetOffset, Vector3.up);
    }

    protected virtual void OnEnemyDeathEvent()
    {
        TryGetComponent(out ScoreEnemy scoreEnemy);

        ScoreManager.Instance.enemiesInGoodOrder.Remove(scoreEnemy);
        ScoreManager.Instance.enemies.Remove(scoreEnemy);
        Destroy(Instantiate(vfxDeath.gameObject, transform.position, Quaternion.identity), vfxDeath.main.startLifetime.constant);
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