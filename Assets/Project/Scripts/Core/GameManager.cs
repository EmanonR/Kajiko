using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool paused; 
    public bool GAMEOVER;
    public float timeSinceAlert = 0;
    public GameObject player;

    public static GameManager Instance;

    public static event Action onPause;
    public static event Action onResume;
    public static event Action onGameOver;

    public bool policeAlerted = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        paused = false;
        onResume?.Invoke();

        ScoreManager.score = 0;

        Instance = this;
    }
    private void Update()
    {
        if (GAMEOVER) return;
        if (paused) return;

        if (Player.dead)
        {
            GAMEOVER = true;
            onGameOver?.Invoke();
            return;
        }

        if (policeAlerted)
            timeSinceAlert += Time.deltaTime;

        if (KeyBinds.Instance.pauseInput.triggered)
        {
            paused = true;
            Time.timeScale = 0f;
            SoundManager.Instance.SwapSnapShot(paused);
            onPause?.Invoke();
        }
    }

    public void InvokeResume()
    {
        paused = false;
        Time.timeScale = 1f;
        SoundManager.Instance.SwapSnapShot(paused);
        onResume?.Invoke();
    }

}
