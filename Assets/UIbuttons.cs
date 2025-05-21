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

 

    public void about()
    {
        Time.timeScale = 0;
        print("working");
        PlayClickSound();

        // Toggle About panel visibility
        bool isActive = About.activeSelf;
        About.SetActive(!isActive);

        // Pause or resume the game based on About panel visibility
        Time.timeScale = isActive ? 1 : 0;
    }

    public void restart()
    {
        AudioListener.pause = false; // Resume all sounds
        PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void mainscene()
    {
        Time.timeScale = 1;
        PlayClickSound();
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
        Time.timeScale = 1;
        PlayClickSound();
        SceneManager.LoadScene("mainmenu");
    }

    public void pause()
    {
       
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
