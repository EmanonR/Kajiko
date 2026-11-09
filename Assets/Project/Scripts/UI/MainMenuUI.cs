using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainPanel;

    public void LoadScene(int i)
    {
        SceneManager.LoadSceneAsync(i);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
