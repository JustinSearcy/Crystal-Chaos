using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int soulsDropped = 1;
    [SerializeField] private int baseScore = 400;
    [SerializeField] private SpriteRenderer enemySprite = null;
    [SerializeField] private GameObject enemyDeathParticles = null;
    [SerializeField] private GameObject floatingPoints = null;

    Color tmpWhite;
    Color tmpRed;

    private bool dead = false;

    private void Start()
    {
        tmpWhite = enemySprite.color;
        tmpRed = enemySprite.color;
        tmpWhite = new Color(255, 255, 255, 0.47f);
        tmpRed = new Color(255, 0, 0, 0.47f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageFlash());
        if(health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }

    IEnumerator DamageFlash()
    {
        enemySprite.color = tmpRed;
        yield return new WaitForSeconds(0.2f);
        enemySprite.color = tmpWhite;
    }

    private void Die()
    {
        GameObject newPointsText = Instantiate(floatingPoints, gameObject.transform.position, Quaternion.identity);
        newPointsText.GetComponentInChildren<TextMeshPro>().text = "" + FindObjectOfType<GameScorer>().GainScore(baseScore);
        Destroy(newPointsText, 2f);
        GameObject newParticles = Instantiate(enemyDeathParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(newParticles, 2f);
        FindObjectOfType<Souls>().GainSouls(soulsDropped);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EnemiesKilled();
        Destroy(gameObject);
    }
}
