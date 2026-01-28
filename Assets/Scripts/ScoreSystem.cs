using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int Score = 0;

    public static event Action<int> OnScoreUpdated;

    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(Coin coin)
    {
        Score += coin.Value;

        OnScoreUpdated?.Invoke(Score);
    }
}