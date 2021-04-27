using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fireballPriceText = null;
    [SerializeField] TextMeshProUGUI speedPriceText = null;
    [SerializeField] TextMeshProUGUI explosionPriceText = null;
    [SerializeField] AudioClip upgradeBoughtSound = null;
    [SerializeField] AudioSource camAudio = null;

    Souls souls;
    PlayerController playerController;

    private int fireballSold = 0;
    private int speedSold = 0;
    private int explosionSold = 0;

    private void Start()
    {
        souls = FindObjectOfType<Souls>();
        playerController = FindObjectOfType<PlayerController>();
        UpdateDisplay();
    }

    public void BuyFireballDamage()
    {
        if(fireballSold < 3)
        {
            int price = GetPrice(fireballSold);
            if (souls.GetSouls() >= price)
            {
                souls.SpendSouls(price);
                fireballSold++;
                UpdateDisplay();
                playerController.IncreaseFireball();
                camAudio.PlayOneShot(upgradeBoughtSound, PlayerPrefs.GetFloat("volume"));
            }
        }
        
    }

    public void BuySpeed()
    {
        if(speedSold < 3)
        {
            int price = GetPrice(speedSold);
            if (souls.GetSouls() >= price)
            {
                souls.SpendSouls(price);
                speedSold++;
                UpdateDisplay();
                playerController.IncreaseSpeed();
                camAudio.PlayOneShot(upgradeBoughtSound, PlayerPrefs.GetFloat("volume"));
            }
        }
    }

    public void BuyExplosionDamage()
    {
        if(explosionSold < 3)
        {
            int price = GetPrice(explosionSold);
            if (souls.GetSouls() >= price)
            {
                souls.SpendSouls(price);
                explosionSold++;
                UpdateDisplay();
                playerController.IncreaseExplosion();
                camAudio.PlayOneShot(upgradeBoughtSound, PlayerPrefs.GetFloat("volume"));
            }
        }
    }

    private int GetPrice(int amtSold)
    {
        return ((amtSold + 1) * 10);
    }

    private void UpdateDisplay()
    {
        if(fireballSold < 3)
        {
            fireballPriceText.text = "Souls: " + GetPrice(fireballSold);
        }
        else
        {
            fireballPriceText.text = "MAX REACHED";
        }

        if (speedSold < 3)
        {
            speedPriceText.text = "Souls: " + GetPrice(speedSold);
        }
        else
        {
            speedPriceText.text = "MAX REACHED";
        }

        if (explosionSold < 3)
        {
            explosionPriceText.text = "Souls: " + GetPrice(explosionSold);
        }
        else
        {
            explosionPriceText.text = "MAX REACHED";
        }
    }
}
