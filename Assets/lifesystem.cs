using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifesystem : MonoBehaviour
{
    public Image[] lifeIcons;         // Assign 5 UI images in the Inspector
    private int currentLives = 5;     // Start with 5 lives (max)

    private void Start()
    {
        UpdateLifeUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("logenemy"))
        {
            if (currentLives > 0)
            {
                currentLives--;
                UpdateLifeUI();
            }

            Destroy(other.gameObject);
        }

        if (other.CompareTag("life"))
        {
            if (currentLives < lifeIcons.Length)
            {
                currentLives++;
                UpdateLifeUI();
            }

            Destroy(other.gameObject);
        }
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < currentLives;
        }
    }
}

