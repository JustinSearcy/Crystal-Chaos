using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] GameObject explosionParticles = null;
    [SerializeField] private float explosionRadius = 0.75f;
    [SerializeField] AudioClip fireballExplosion = null;
    [SerializeField] AudioSource camAudio = null;

    private void Start()
    {
        camAudio = Camera.main.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(collision.gameObject.tag == "Player")
        {
            return;
        }
        else if(collision.gameObject.tag != "Wall")
        {
            CreateExplosion(playerController.fireballSplashDamage);
            camAudio.PlayOneShot(fireballExplosion, PlayerPrefs.GetFloat("volume"));
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(playerController.fireballDamage);
            }
        }
    }

    private void CreateExplosion(int fireballSplashDamage)
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(gameObject.transform.position, explosionRadius);
        foreach (Collider2D collider in hitObjects)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(fireballSplashDamage);
            }
        }

        ExplosionEffects();
    }

    private void ExplosionEffects()
    {
        GameObject explosion = Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        Destroy(this.gameObject);
    }
}
