using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScorer : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText = null;

    private int roundScore = 0;

    private void Start()
    {
        scoreText.text = "Score: " + currentScore;
    }

    public int GainScore(int amount)
    {
        int currentCrystalCount = FindObjectOfType<GameManager>().GetCrystalCount();
        int scoreEarned = amount * currentCrystalCount;
        currentScore += scoreEarned;
        UpdateDisplay();
        return scoreEarned;
    }

    private void UpdateDisplay()
    {
        scoreText.text = "Score: " + currentScore;
    }

    public int GameOver()
    {
        roundScore = currentScore;
        return roundScore;
    }
}
