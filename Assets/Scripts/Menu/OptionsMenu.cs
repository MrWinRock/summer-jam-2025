using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void Resume()
    {
        TogglePause(false);
    }

    public void Reset()
    {
        TogglePause(false);

        if (SceneManager.GetActiveScene().name == "Wild_West")
        {
            FindObjectOfType<PlayerHealth>()?.Die();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void MainMenu()
    {
        TogglePause(false);
        SceneManager.LoadScene(0);
    }

    private void TogglePause()
    {
        bool isPaused = !optionsMenu.activeSelf;
        optionsMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            if (isPaused)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }
    }
    
    private void TogglePause(bool isPaused)
    {
        optionsMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            if (isPaused)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }
    }
}