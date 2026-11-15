using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player player;

    public GameObject PausePanel;
    public GameObject UIPanel;
    public GameObject GameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text FuelGaugeText;
    public Image FuelGauge;
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

        GameManager.OnPause += PauseMenu;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        UIPanel.SetActive(true);
        FlameThrowerFuel.OnFuelChanged += UpdateAmmoUI;
        GameManager.OnGameOver += GameOverEvent;
        ScoreManager.OnScoreChanged += UpdateScore;

        player = GameManager.Instance.player.GetComponent<Player>();
        player.OnHealthChanged += UpdateHP;

    }

    private void Start()
    {
        UpdateScore();
    }

    private void OnDestroy()
    {
        GameManager.OnPause -= PauseMenu;
        FlameThrowerFuel.OnFuelChanged -= UpdateAmmoUI;
        GameManager.OnGameOver -= GameOverEvent;
        ScoreManager.OnScoreChanged -= UpdateScore;
        player.OnHealthChanged -= UpdateHP;
    }

    void GameOverEvent()
    {
        PausePanel.SetActive(false);
        UIPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    void UpdateHP(float current, float max)
    {
        hpText.text = $"{current:f0}/{max}";
    }

    public void UpdateAmmoUI(float current, float max)
    {
        FuelGaugeText.text = current.ToString("f0") + "/" + max.ToString("f0");
        FuelGauge.fillAmount = current / max;
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
