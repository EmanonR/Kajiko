using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelingManager : MonoBehaviour
{
    public Slider levelSlider;
    public int baseEXP = 100;
    public float targetIncrease = 1.5f;

    int targetEXP;
    int currentEXP;
    int currentLevel = 1;

    public static event Action OnLevelUp;

    private void Start()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore()
    {
        int remove = 0;

        for (int i = 1; i < currentLevel; i++)
        {
            remove += DetermineEXPPerLevel(i);
        }

        int amount = (int)ScoreManager.GetScore() - remove;

        targetEXP = DetermineEXPPerLevel(currentLevel);
        SetEXP(amount);
        levelSlider.value = (float)amount / (float)targetEXP;
    }

    int DetermineEXPPerLevel(int level)
    {
        return (int)(baseEXP * Mathf.Pow(targetIncrease, level - 1));
    }

    public void SetEXP(int incoming)
    {
        currentEXP = incoming;
        if (currentEXP >= targetEXP)
        {
            IncreaseLevel();
            UpdateScore();
        }
    }

    public void IncreaseLevel()
    {
        OnLevelUp?.Invoke();
        currentLevel++;
    }
}
