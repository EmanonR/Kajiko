using UnityEngine;

public class ScoreManager
{
    public static float score = 0;

    public static void AddScore(float i)
    {
        score += i;
    }

    public static void RemoveScore(float i)
    {
        score -= i;
    }

    public static float GetScore()
    {
        return score;
    }
}
