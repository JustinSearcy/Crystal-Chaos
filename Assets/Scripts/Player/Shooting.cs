using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform shotPoint = null;
    [SerializeField] GameObject fireballPrefab = null;
    [SerializeField] private float fireballSpeed = 10f;
    [SerializeField] private AudioClip fireballShoot = null;
    [SerializeField] private AudioSource camAudio = null;

    PlayerController playerController;
    GameManager gameManager;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!playerController.teleportActive && Input.GetKeyDown(KeyCode.Space) && gameManager.gameActive)
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        Vector3 fireballPos = new Vector3(shotPoint.transform.position.x, shotPoint.transform.position.y, -1);
        camAudio.PlayOneShot(fireballShoot, PlayerPrefs.GetFloat("volume"));
        GameObject newFireball = Instantiate(fireballPrefab, fireballPos, shotPoint.rotation);
        Rigidbody2D rb = newFireball.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.right * fireballSpeed, ForceMode2D.Impulse);
    }
}
