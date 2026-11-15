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

    public static event Action OnLeveUp;

    private void Start()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    private void UpdateScore()
    {
        int remove = 0;

        for (int i = 0; i < currentLevel; i++)
        {
            remove += DetermineEXPPerLevel(i);
        }

        int amount = (int)ScoreManager.GetScore() - remove;

        targetEXP = DetermineEXPPerLevel(currentLevel);
        IncreaseEXP(amount);
        levelSlider.value = (float)amount / (float)targetEXP;
    }

    int DetermineEXPPerLevel(int level)
    {
        return (int)(baseEXP * (targetIncrease * level));
    }

    public void IncreaseEXP(int incoming)
    {
        currentEXP = incoming;
        if (currentEXP >= targetEXP)
        {
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        OnLeveUp?.Invoke();
        currentLevel++;
    }
}
