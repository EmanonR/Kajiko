using System;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class GameManager : MonoBehaviour
{
    public Player player;

    public static GameManager Instance;

    public static event Action OnPause;
    public static event Action OnResume;
    public static event Action OnGameOver;
    public static event Action OnCardSelection;

    public GameState gameState;

    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        ChoosingCards
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;
        gameState = GameState.Playing;

        ScoreManager.ResetScore();
        PlayerStats.ResetStats();

        player.OnDeath += OnDeath;
        ScoreManager.Initialize();
    }
    private void OnDestroy()
    {
        if (player != null)
            player.OnDeath -= OnDeath;
    }

    public void SetCardSelecting()
    {
        Time.timeScale = 0f;
        gameState = GameState.ChoosingCards;
        OnCardSelection?.Invoke();
    }


    private void Update()
    {
        if (gameState != GameState.Playing) return;

        if (KeyBinds.Instance.pauseInput.triggered)
        {
            gameState = GameState.Paused;
            Time.timeScale = 0f;
            OnPause?.Invoke();
        }
    }

    void OnDeath()
    {
        gameState = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    public void InvokeResume()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1f;
        OnResume?.Invoke();
    }

}
