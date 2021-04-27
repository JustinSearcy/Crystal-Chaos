using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Souls : MonoBehaviour
{
    [SerializeField] private int currentSouls = 0;
    [SerializeField] private TextMeshProUGUI soulsText = null;

    private void Start()
    {
        soulsText.text = "Souls: " + currentSouls;
    }

    public void GainSouls(int amount)
    {
        currentSouls += amount;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        soulsText.text = "Souls: " + currentSouls;
    }

    public int GetSouls() { return currentSouls; }

    public void SpendSouls(int amount)
    {
        currentSouls -= amount;
        UpdateDisplay();
    }
}
