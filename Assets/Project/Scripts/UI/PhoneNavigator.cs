using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneNavigator : MonoBehaviour
{
    [SerializeField] Animator phoneAnimator;

    [SerializeField] bool open;

    public InputAction phoneKey;
    public InputAction phoneNav;

    private void Awake()
    {
        phoneKey.Enable();
        phoneNav.Enable();
        PhoneFlip();
    }

    private void Update()
    {
        if (phoneKey.triggered)
        {
            open = !open;
            PhoneFlip();
        }
    }

    void PhoneFlip()
    {
        if (open)
            phoneAnimator.Play("PhoneOpen");
        else
            phoneAnimator.Play("PhoneClosed");
    }
}
