using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIbuttons : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject About;
    [SerializeField] private GameObject banner;
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSource;

    void Start()
    {
        Time.timeScale = 1; // Ensure game is unpaused at start
        AudioListener.pause = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void about()
    {
        PlayClickSound();

        bool isActive = About.activeSelf;
        About.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
        AudioListener.pause = isActive ? false : true;
    }

    public void restart()
    {
        PlayClickSound();
        AudioListener.pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainscene()
    {
        PlayClickSound();
        AudioListener.pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("main");
    }

    public void quitt()
    {
        PlayClickSound();
        Application.Quit();
    }

    public void mainmenu()
    {
        PlayClickSound();
        AudioListener.pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("mainmenu");
    }

    public void pause()
    {
        PlayClickSound();
        Time.timeScale = 0;
        AudioListener.pause = true; // Pause all audio
        if (banner != null)
            banner.SetActive(true);
    }

    public void resume()
    {
        PlayClickSound();
        Time.timeScale = 1;
        AudioListener.pause = false; // Resume all audio
        if (banner != null)
            banner.SetActive(false);
    }
}
