using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Player player;

    public GameObject PausePanel;
    public GameObject UIPanel;
    public GameObject GameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text FuelGaugeText;
    public TMP_Text hpText;

    public static CanvasManager singleton;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }

        singleton = this;

        GameManager.onPause += PauseMenu;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        UIPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.onPause -= PauseMenu;
    }

    private void Update()
    {
        if (GameManager.singleton.GAMEOVER)
        {
            PausePanel.SetActive(false);
            UIPanel.SetActive(false);
            GameOverPanel.SetActive(true);
            return;
        }
        UpdateScore();
        UpdateHP();
    }

    void UpdateHP()
    {
        hpText.text = "HP: " + player.currentHP.ToString("f0");
    }

    public void UpdateAmmoUI(float current, float max)
    {
        FuelGaugeText.text = current.ToString("f0") + "/" + max.ToString("f0");
    }

    void UpdateScore()
    {
        scoreText.text = ScoreManager.GetScore().ToString("f0");
    }

    public void PauseMenu()
    {
        PausePanel.SetActive(true);
        UIPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        GameManager.singleton.InvokeResume();
        PausePanel.SetActive(false);
        UIPanel.SetActive(true);
    }

    public void LoadScene(int i)
    {
        GameManager.singleton.InvokeResume();
        SceneManager.LoadSceneAsync(i);
        Cursor.lockState = CursorLockMode.None;
    }
}
