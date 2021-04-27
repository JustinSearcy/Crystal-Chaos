using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHealth : MonoBehaviour
{
    [SerializeField] private int crystalHealth = 10;
    [SerializeField] private GameObject crystalDestroyedParticles = null;
    [SerializeField] private AudioClip crystalDestroyed = null;
    [SerializeField] private AudioClip crystalHit = null;
    [SerializeField] private AudioSource camAudio = null;
    [SerializeField] private Transform healthBar = null;

    public void TakeDamage(int amount)
    {
        camAudio.PlayOneShot(crystalHit, PlayerPrefs.GetFloat("volume"));
        crystalHealth -= amount;
        UpdateHealthBar();
        if(crystalHealth <= 0)
        {
            CrystalDestroyed();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.localScale = new Vector3(crystalHealth * 0.1f, 1f, 1f);
    }

    private void CrystalDestroyed()
    {
        camAudio.PlayOneShot(crystalDestroyed, PlayerPrefs.GetFloat("volume"));
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.CrystalDestroyed(this.gameObject);
        gameManager.ShakeCam();
        GameObject newParticles = Instantiate(crystalDestroyedParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(newParticles, 2f);
        Destroy(this.gameObject);
    }
}
