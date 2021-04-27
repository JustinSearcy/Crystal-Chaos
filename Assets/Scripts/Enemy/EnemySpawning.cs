using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawners = null;
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] float spawnRate = 1.7f;
    [SerializeField] float spawnTimer = 1.7f;

    GameManager gameManager;

    private float enemyMoveSpeed = 1f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(spawnTimer <= 0 && gameManager.gameActive)
        {
            SpawnEnemy();
            spawnTimer = spawnRate;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        int randSpawner = UnityEngine.Random.Range(0, spawners.Count);
        Instantiate(enemyPrefab, spawners[randSpawner].transform.position, Quaternion.identity);
    }

    public void IncreaseRate()
    {
        if(spawnRate > 0.8f)
        {
            spawnRate -= 0.15f;
        }
        else if(enemyMoveSpeed < 2.5f)
        {
            enemyMoveSpeed += 0.15f;
        }
        else
        {
            return;
        }
    }

    public float GetEnemySpeed() { return enemyMoveSpeed; }
}
