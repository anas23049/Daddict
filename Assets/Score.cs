using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;       // Reference to the player
    public Text scoreText;         // Displays current score
    public Text highScoreText;     // Displays high score

    private float startx;          // Starting position of the player

    void Start()
    {
        

        startx = player.position.z;

        // Load high score from PlayerPrefs
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "" + savedHighScore;
    }
    public Text gameplayCoinText;
    private int levelCoins = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("coin"))
        {
            Destroy(collider.gameObject);

            levelCoins++;
            gameplayCoinText.text = "" + levelCoins;

            if (coinmanager.Instance != null)
            {
                coinmanager.Instance.AddCoin(1);
            }
        }
    }
    void Update()
    {
        float distanceMoved = player.position.x - startx;
        int currentScore = Mathf.FloorToInt(distanceMoved);

        scoreText.text = "" + currentScore;

        // Check and update high score
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
            highScoreText.text = "" + currentScore;
        }
    }
}
