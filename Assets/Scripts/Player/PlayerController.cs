using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] public int fireballDamage = 3;
    [SerializeField] public int fireballSplashDamage = 2;

    [Header("Teleport")]
    [SerializeField] private GameObject crystalTeleportImage = null;
    [SerializeField] private GameObject teleportParticles = null;
    [SerializeField] private AudioClip teleportSound = null;
    [SerializeField] AudioSource camAudio = null;

    [Header("Menus")]
    [SerializeField] private GameObject soulWellMenu = null;
    [SerializeField] private GameObject optionsMenu = null;
 
    Rigidbody2D rb;

    GameObject selectedCrystal;

    GameManager gameManager;

    Vector2 movement;
    Vector2 pos;

    public bool teleportActive = false;
    public bool soulWellInteractable = false;
    public bool teleportAllowed = false;
    public bool menuOpen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameActive)
        {
            GetMovement();
            Teleport();
            DisplayTeleportImage();
            InteractWithSoulWell();
        }
        OptionsMenu();
    }

    private void GetMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        pos = rb.position + movement.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(pos);
    }

    private void Teleport()
    {
        if(teleportActive && teleportAllowed && Input.GetMouseButtonDown(0))
        {
            camAudio.PlayOneShot(teleportSound, PlayerPrefs.GetFloat("volume"));
            Vector2 teleportPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DisplayParticles(teleportPos);
            selectedCrystal.transform.position = teleportPos;  
            crystalTeleportImage.SetActive(false);
            teleportActive = false;
            selectedCrystal.GetComponent<Crystal>().DeselectCrystal();
            selectedCrystal = null;
            UpdateEnemies();
        }
    }

    private void DisplayParticles(Vector2 teleportPos)
    {
        GameObject teleportParticlesOrig = Instantiate(teleportParticles, selectedCrystal.transform.position, Quaternion.identity);
        GameObject teleportParticlesNew = Instantiate(teleportParticles, teleportPos, Quaternion.identity);
        Destroy(teleportParticlesOrig, 2f);
        Destroy(teleportParticlesNew, 2f);
    }

    public void UpdateEnemies()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.FindTarget();
        }
    }

    private void DisplayTeleportImage()
    { 
        if (teleportActive)
        {
            crystalTeleportImage.SetActive(true);
            Vector2 teleportPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            crystalTeleportImage.transform.position = teleportPos;
        }
    }

    private void InteractWithSoulWell()
    {
        if(soulWellInteractable && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            soulWellMenu.SetActive(true);
            menuOpen = true;
        }
    }

    private void OptionsMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            optionsMenu.SetActive(true);
            FindObjectOfType<VolumeController>().UpdateDisplay();
            menuOpen = true;
        }
    }

    public void SelectCrystal(GameObject crystal)
    {
        selectedCrystal = crystal;
        StartCoroutine(CrystalSelected());  
    }

    IEnumerator CrystalSelected()
    {
        yield return new WaitForSeconds(0.1f);
        teleportActive = true;
    }

    public void IncreaseFireball()
    {
        fireballDamage += 2;
    }

    public void IncreaseSpeed()
    {
        moveSpeed += 0.5f;
    }

    public void IncreaseExplosion()
    {
        fireballSplashDamage += 1;
    }
}
