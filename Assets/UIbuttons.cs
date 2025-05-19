using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIbuttons : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject About;
    [SerializeField] GameObject banner;
    [SerializeField] AudioClip clickSound; // Sound to play on button click

    private AudioSource audioSource;
    private bool isMenuActive = false;

    void Start()
    {
        // Add or get an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void ToggleMenu()
    {
        PlayClickSound();
        if (isMenuActive)
        {
            buttons.SetActive(false);
            Time.timeScale = 1; // Resume game time
            isMenuActive = false;
        }
        else
        {
            buttons.SetActive(true);
            Time.timeScale = 1; // Pause game time
            isMenuActive = true;
        }
    }

    public void about()
    {
        PlayClickSound();
        About.SetActive(true);
        Time.timeScale = 0;
    }

    public void restart()
    {
        AudioListener.pause = false; // Resume all sounds
        PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void mapscene()
    {
        AudioListener.pause = false; // Resume all sounds
        AudioListener.volume = 1;
        PlayClickSound();
        SceneManager.LoadScene("maps");
        Time.timeScale = 1;
        
    }
    public void garagescene()
    {
        AudioListener.pause = false; // Resume all sounds
        PlayClickSound();
        SceneManager.LoadScene("garage");
        Time.timeScale = 1;
    }

    public void quitt()
    {
        PlayClickSound();
        Application.Quit();
    }

    public void level1()
    {
        PlayClickSound();
        SceneManager.LoadScene("level1");
    }

    public void pause()
    {
        Debug.Log("Pause button pressed");
        PlayClickSound();
        Time.timeScale = 0;
        banner.SetActive(true);
    }




    public void resume()
    {
        AudioListener.pause = false; // Resume all sounds
        PlayClickSound();
        Time.timeScale = 1;
        banner.SetActive(false);
    }
}
