using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinmanager : MonoBehaviour
{
    public static coinmanager Instance;
    private int totalCoins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load previously saved coins
            totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins); // Save immediately
        PlayerPrefs.Save();
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }
}
