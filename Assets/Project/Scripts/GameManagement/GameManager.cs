using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public bool paused; 
    public bool GAMEOVER;
    public float timeSinceAlert = 0;
    public GameObject player;

    public InputAction pauseButton;

    public static GameManager singleton;

    public static event Action onPause;
    public static event Action onResume;

    public bool policeAlerted = false;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }

        paused = false;
        onResume?.Invoke();

        ScoreManager.score = 0;

        singleton = this;

        pauseButton.Enable();
    }
    private void Update()
    {
        if (GAMEOVER) return;
        if (paused) return;

        if (Player.dead) GAMEOVER = true;

        if (policeAlerted)
            timeSinceAlert += Time.deltaTime;

        if (pauseButton.triggered)
        {
            paused = true;
            Time.timeScale = 0f;
            SoundManager.instance.SwapSnapShot(paused);
            onPause?.Invoke();
        }
    }

    public void InvokeResume()
    {
        paused = false;
        Time.timeScale = 1f;
        SoundManager.instance.SwapSnapShot(paused);
        onResume?.Invoke();
    }

}
