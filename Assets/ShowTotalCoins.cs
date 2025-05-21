using UnityEngine;
using UnityEngine.UI;

public class ShowTotalCoins : MonoBehaviour
{
    public Text totalCoinText;

    void Start()
    {
        int savedCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinText.text = "" + savedCoins;
    }
}
