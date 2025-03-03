using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    void Start()
    {
        UpdateScoreText();
    }

    // Method to increment the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Update the score text UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
