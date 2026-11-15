using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeSinceAlert = 0;
    public GameObject player;

    public static GameManager Instance;

    public static event Action onPause;
    public static event Action onResume;
    public static event Action onGameOver;

    public bool policeAlerted = false;

    public GameState gameState;

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Time.timeScale = 1f;
        gameState = GameState.Playing;
        onResume?.Invoke();

        ScoreManager.ResetScore();

        Instance = this;
    }
    private void Update()
    {
        if (gameState != GameState.Playing) return;

        if (Player.dead)
        {
            gameState = GameState.GameOver;
            onGameOver?.Invoke();
            return;
        }

        if (policeAlerted)
            timeSinceAlert += Time.deltaTime;

        if (KeyBinds.Instance.pauseInput.triggered)
        {
            gameState = GameState.Paused;
            Time.timeScale = 0f;
            onPause?.Invoke();
        }
    }

    public void InvokeResume()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1f;
        onResume?.Invoke();
    }

}
