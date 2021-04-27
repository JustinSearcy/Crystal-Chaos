using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> activeCrystals = new List<GameObject>(4);
    [SerializeField] GameObject GameOverCanvas = null;
    [SerializeField] GameObject newHighScore = null;
    [SerializeField] GameObject noHighScore = null;
    [SerializeField] TextMeshProUGUI roundScoreText = null;
    [SerializeField] TextMeshProUGUI oldHighScoreText = null;

    private int enemiesKilled = 0;

    GameScorer gameScorer;

    Animator animUI;
    Animator anim;

    const string HIGH_SCORE = "high-score";

    public bool gameActive = true;

    private void Start()
    {
        gameScorer = FindObjectOfType<GameScorer>();
        anim = GameObject.Find("Main Camera").GetComponent<Animator>();
        animUI = GameOverCanvas.GetComponent<Animator>();
    }

    public List<GameObject> GetActiveCrystals() { return activeCrystals; }

    public int GetCrystalCount() { return activeCrystals.Count; }

    public void CrystalDestroyed(GameObject crystal)
    {
        activeCrystals.Remove(crystal);
        if(activeCrystals.Count <= 0)
        {
            gameActive = false;
            animUI.SetTrigger("DropIn");
            int roundScore = gameScorer.GameOver();
            HighScore(roundScore);
            roundScoreText.text = "" + roundScore;
        }
        else
        {
            FindObjectOfType<PlayerController>().UpdateEnemies();
        }
    }

    private void HighScore(int roundScore)
    {
        InitializeHighScore();
        if (roundScore > PlayerPrefs.GetInt(HIGH_SCORE))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, roundScore);
            newHighScore.SetActive(true);
        }
        else
        {
            noHighScore.SetActive(true);
            oldHighScoreText.text = "" + PlayerPrefs.GetInt(HIGH_SCORE);
        }
    }

    public static void InitializeHighScore()
    {
        if (!PlayerPrefs.HasKey(HIGH_SCORE))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
        }
        else
        {
            return;
        }
    }

    public void ShakeCam()
    {
        anim.SetTrigger("Shake");
    }

    public void EnemiesKilled()
    {
        enemiesKilled++;
        if(enemiesKilled % 10 == 0)
        {
            FindObjectOfType<EnemySpawning>().IncreaseRate();
        }
    }
}
