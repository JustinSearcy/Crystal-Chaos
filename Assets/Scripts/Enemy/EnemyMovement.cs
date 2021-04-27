using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 1f;
    [SerializeField] private float attackDistance = 0.7f;
    [SerializeField] private float linearMoveDistance = 2f;

    [SerializeField] private Transform targetCrystal; //For debug

    [SerializeField] Transform enemySprite = null;

    [SerializeField] Animator anim = null;

    [SerializeField] float frequency = 0.3f;
    [SerializeField] float amplitude = 1f;
    private float startTime;
    private Vector3 direction;
    private Vector3 orthogonal;
    Rigidbody2D rb;

    private List<GameObject> activeCrystals;

    GameManager gameManager;

    private float distanceToTarget;
    private float randomFactor;

    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();

        //randomFactor = UnityEngine.Random.Range(0f, 5f);

        gameManager = FindObjectOfType<GameManager>();

        enemyMoveSpeed = FindObjectOfType<EnemySpawning>().GetEnemySpeed();

        if(targetCrystal == null)
        {
            FindTarget();
        }

    }

    public void FindTarget()
    {
        GameObject closestCrystal = null;
        float closestDistance = Mathf.Infinity;
        activeCrystals = gameManager.GetActiveCrystals();
        for (int i = 0; i < activeCrystals.Count; i++)
        {
            float distance = Vector2.Distance(gameObject.transform.position, activeCrystals[i].transform.position);
            if (distance < closestDistance)
            {
                closestCrystal = activeCrystals[i];
                closestDistance = distance;
            }
        }
        if(closestCrystal != null)
        {
            targetCrystal = closestCrystal.transform;
        }
    }

    void Update()
    {
        if(targetCrystal != null)
        {
            distanceToTarget = Vector2.Distance(gameObject.transform.position, targetCrystal.position);
            if (distanceToTarget > linearMoveDistance)
            {
                MoveSin();
            }
            else if (distanceToTarget > attackDistance)
            {
                MoveLinear();
            }
            else
            {
                Attack();
            }
        }
    }

    private void MoveSin()
    {
        direction = (targetCrystal.position - transform.position).normalized;
        orthogonal = new Vector3(-direction.z, direction.x, 0);
        float t = Time.time - startTime;
        rb.velocity = direction * enemyMoveSpeed + orthogonal * amplitude * Mathf.Sin(frequency * t);


        anim.SetBool("isRunning", true);
        //this.gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetCrystal.position, enemyMoveSpeed * Time.deltaTime);
        //this.gameObject.transform.position += transform.right * Mathf.Sin(Time.time * frequency + randomFactor) * amplitude;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (gameObject.transform.position.x - targetCrystal.position.x > 0)
        {
            enemySprite.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            enemySprite.localScale = Vector3.one;
        }
    }

    private void MoveLinear()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("isRunning", true);
        this.gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetCrystal.position, enemyMoveSpeed * Time.deltaTime);
        UpdateSprite();
    }

    private void Attack()
    {
        anim.SetBool("isRunning", false);
    }

    public bool InAttackRange()
    {
        if (distanceToTarget < attackDistance && targetCrystal != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Transform GetTargetCrystal() { return targetCrystal; }
}
