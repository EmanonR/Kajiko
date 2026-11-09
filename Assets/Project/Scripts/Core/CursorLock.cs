using UnityEngine;

public class CursorLock : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.onPause += EventPause;
        GameManager.onResume += EventResume;
    }

    private void OnDestroy()
    {
        GameManager.onPause -= EventPause;
        GameManager.onResume -= EventResume;
    }

    private void Update()
    {
        if (GameManager.Instance.GAMEOVER)
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
