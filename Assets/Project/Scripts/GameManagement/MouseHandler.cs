using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHandler : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.onPause += EventPause;
        GameManager.onResume += EventResume;
    }

    private void Update()
    {
        if (GameManager.singleton.GAMEOVER)
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
