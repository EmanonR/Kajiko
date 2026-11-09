using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Player player;

    public GameObject PausePanel;
    public GameObject UIPanel;
    public GameObject GameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text FuelGaugeText;
    public TMP_Text hpText;

    public static HUD Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

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
        if (GameManager.Instance.GAMEOVER)
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
        GameManager.Instance.InvokeResume();
        PausePanel.SetActive(false);
        UIPanel.SetActive(true);
    }

    public void LoadScene(int i)
    {
        GameManager.Instance.InvokeResume();
        SceneManager.LoadSceneAsync(i);
        Cursor.lockState = CursorLockMode.None;
    }
}
