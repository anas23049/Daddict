using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public Text gameoverscoreText;
    public Text highScoreText;
    public Text gameplayCoinText;
    public Text gameoverCoinText;

    public AudioSource audioSource;    // Assign in Inspector
    public AudioClip coinSound;        // Assign your coin sound clip

    public float scoreMultiplier = 20f; // Multiplier to increase score speed

    private int levelCoins = 0;
    private float startx;

    void Start()
    {
        startx = player.position.x; // Adjust if your player moves along a different axis

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "" + savedHighScore;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("coin"))
        {
            Destroy(collider.gameObject);

            levelCoins++;
            gameplayCoinText.text = "" + levelCoins;
            gameoverCoinText.text = "" + levelCoins;

            if (audioSource != null && coinSound != null)
                audioSource.PlayOneShot(coinSound);

            coinmanager.Instance.AddCoin(1);
        }
    }

    void Update()
    {
        float distanceMoved = player.position.x - startx; // Change to player.position.z if needed
        int currentScore = Mathf.FloorToInt(distanceMoved * scoreMultiplier);

        scoreText.text = "" + currentScore;
        gameoverscoreText.text = "" + currentScore;

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
            highScoreText.text = "" + currentScore;
        }
    }
}
