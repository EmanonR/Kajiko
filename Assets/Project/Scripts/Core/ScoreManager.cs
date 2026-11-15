using System;

public static class ScoreManager
{
    public static float score = 0;

    public static event Action OnScoreChanged;

    public static void AddScore(float i)
    {
        score += i;
        OnScoreChanged?.Invoke();
    }

    public static void RemoveScore(float i)
    {
        score -= i;
        OnScoreChanged?.Invoke();
    }

    public static float GetScore()
    {
        return score;
    }
}
