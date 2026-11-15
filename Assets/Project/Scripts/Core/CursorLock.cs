using UnityEngine;

public class CursorLock : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.OnPause += EventPause;
        GameManager.OnResume += EventResume;
        GameManager.OnGameOver += OnGameOver;
        GameManager.OnCardSelection += OnCard;
    }

    private void OnDestroy()
    {
        GameManager.OnPause -= EventPause;
        GameManager.OnResume -= EventResume;
        GameManager.OnGameOver -= OnGameOver;
        GameManager.OnCardSelection -= OnCard;
    }

    void OnCard()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void OnGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void EventPause()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void EventResume()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
