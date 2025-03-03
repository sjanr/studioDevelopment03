using TMPro; // Import TextMeshPro namespace
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0; // Current score
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    void Start()
    {
        // Initialize the score text when the game starts
        UpdateScoreText();
    }

    // Method to increment the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText(); // Update the score text after adding the score
    }

    // Update the score text UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Update text with current score
    }
}
