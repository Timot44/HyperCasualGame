using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("TOUCH SETTINGS")] public GameObject trailFinger;
    public float distanceFromCamera = 5;
    public LayerMask layerEnemy;

    [Header("SCORE")] public ScoreManager scoreManager;
    public float score;
    public float multiplierToAdd = 1f;
    public float speedToAddIfWrongEnemy;
    
    public TextMeshPro textScore;
    public TextMeshPro textWave;

    public GameObject floatingText;
    
    private float _multiplierToAddMax;
    
    private Camera cam;
    private static GameManager _gameManager;
    public static GameManager Instance => _gameManager;

    [Header("GAME ENDING PARAMETERS")] [SerializeField]
    private GameObject panelGameOver;

    [SerializeField] private Volume postProcess;
    private DepthOfField _depthOfField;
    public bool isGameOver;

    private void Awake()
    {
        _gameManager = this;
        postProcess.profile.Reset();
    }

    private void Start()
    {
        _multiplierToAddMax = multiplierToAdd;
        cam = Camera.main;
        if (postProcess.profile.TryGet(out DepthOfField depthOfField)) _depthOfField = depthOfField;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EnemySpawnerManager.Instance.isAllEnemiesSpawned)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerEnemy))
            {
                hit.collider.TryGetComponent(out Enemy enemy);
                hit.collider.TryGetComponent(out ScoreEnemy scoreEnemy);

                if (scoreEnemy.numberOfEnemy != scoreManager.enemiesInGoodOrder[0].numberOfEnemy)
                {
                    enemy.moveSpeed += speedToAddIfWrongEnemy;
                    var enemyTransform = enemy.transform;
                    Destroy(Instantiate(enemy.vfxWrongParticle.gameObject, enemyTransform.position, Quaternion.identity, enemyTransform), enemy.vfxWrongParticle.main.startLifetime.constant);
                }
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            trailFinger.SetActive(true);
            MoveTrailToCursor(Input.mousePosition);
        }
        else
        {
            trailFinger.SetActive(false);
            MoveTrailToCursor(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && scoreManager.enemiesInGoodOrder.Count > 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerEnemy))
            {
                hit.collider.TryGetComponent(out ScoreEnemy scoreEnemy);
                hit.collider.TryGetComponent(out Enemy enemy);

                if (scoreEnemy.numberOfEnemy == scoreManager.enemiesInGoodOrder[0].numberOfEnemy)
                {
                    scoreManager.enemiesInGoodOrder.Remove(scoreEnemy);
                    scoreManager.enemies.Remove(scoreEnemy);

                    enemy.enemyHealth.TakeDamage(1);

                    hit.collider.gameObject.SetActive(false);
                    
                    score++;
                    multiplierToAdd++;

                    score += multiplierToAdd;

                    var scoreCap = Mathf.Ceil(score);

                    textScore.text = $"Score : {scoreCap}";

                    var floating = Instantiate(floatingText, hit.transform.position + new Vector3(0,3,0), Quaternion.identity);
                    floating.GetComponent<TextMeshPro>().text = $"+{multiplierToAdd}";
                }
                else
                {
                    AudioManager.Instance.Play("ErrorSound");
                    multiplierToAdd = _multiplierToAddMax;
                }
            }
        }
    }

    private void MoveTrailToCursor(Vector3 screenPosition)
    {
        trailFinger.transform.position =
            cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
    }

    public void SetUpGameOver()
    {
        _depthOfField.active = true;
        if (EnemySpawnerManager.Instance != null)
        {
            foreach (var enemy in EnemySpawnerManager.Instance.currentEnemiesInWave) enemy.gameObject.SetActive(false);
        }

        panelGameOver.SetActive(true);
        isGameOver = true;
    }
}