using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class KeyBinds : MonoBehaviour
{
    [Header("Gameplay")]
    public InputAction movementInput;
    public InputAction sprintInput;
    public InputAction shootInput;
    public InputAction aimInput;
    public InputAction reloadInput;

    [Header("Camera")]
    public InputAction scrollInput;
    public InputAction mouseInput;

    [Header("Phone and interactions")]
    public InputAction phoneNavigationInput;
    public InputAction phoneOpenInput;
    public InputAction interactInput;
    public InputAction pauseInput;

    public static KeyBinds Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    private void OnEnable() => EnableAll();

    void EnableAll()
    {
        movementInput.Enable();
        shootInput.Enable();
        aimInput.Enable();
        scrollInput.Enable();
        mouseInput.Enable();
        reloadInput.Enable();
        phoneNavigationInput.Enable();
        phoneOpenInput.Enable();
        interactInput.Enable();
        pauseInput.Enable();
    }

    private void OnDisable()
    {
        movementInput.Disable();
        shootInput.Disable();
        aimInput.Disable();
        scrollInput.Disable();
        mouseInput.Disable();
        reloadInput.Disable();
        phoneNavigationInput.Disable();
        phoneOpenInput.Disable();
        interactInput.Disable();
        pauseInput.Disable();
    }
}
