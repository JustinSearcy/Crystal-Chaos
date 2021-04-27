using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] AudioSource camAudio = null;
    [SerializeField] AudioClip selectSound = null;

    private void Start()
    {
        camAudio = Camera.main.GetComponent<AudioSource>();
    }

    public void Quit()
    {
        camAudio.PlayOneShot(selectSound, PlayerPrefs.GetFloat("volume"));
        Application.Quit();
    }

    public void Restart()
    {
        camAudio.PlayOneShot(selectSound, PlayerPrefs.GetFloat("volume"));
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        camAudio.PlayOneShot(selectSound, PlayerPrefs.GetFloat("volume"));
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        camAudio.PlayOneShot(selectSound, PlayerPrefs.GetFloat("volume"));
        Time.timeScale = 1;
        FindObjectOfType<PlayerController>().menuOpen = false;
    }

    public void StartGame()
    {
        camAudio.PlayOneShot(selectSound, PlayerPrefs.GetFloat("volume"));
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
