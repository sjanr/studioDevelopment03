using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;

    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score: {score}");
    }
}
