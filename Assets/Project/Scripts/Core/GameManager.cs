using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool paused; 
    public bool GAMEOVER;
    public float timeSinceAlert = 0;
    public GameObject player;

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
    }
    private void Update()
    {
        if (GAMEOVER) return;
        if (paused) return;

        if (Player.dead) GAMEOVER = true;

        if (policeAlerted)
            timeSinceAlert += Time.deltaTime;

        if (KeyBinds.Instance.pauseInput.triggered)
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
