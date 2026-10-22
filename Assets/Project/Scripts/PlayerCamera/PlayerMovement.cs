using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float walkSpeed = 8;
    public bool canJump = false;
    public float jumpPower = 5, jumpTime = 1f;
    public float turnSmoothTime = .1f;

    public static MovementState movementState;

    float currentSpeed;
    float _yVelocity;
    bool aiming, shooting;
    float turnSmoothVel;
    float targetAngle;

    public enum MovementState
    {
        Idle,
        Walking,
        Running
    }
    private void Awake()
    {
        FlameThrower.onAim += OnAimEvent;
        FlameThrower.onShoot += OnShootvent;
    }

    private void Update()
    {

        if (GameManager.singleton.GAMEOVER) return;
        if (GameManager.singleton.paused) return;

        currentSpeed = InputSystem.actions["Sprint"].ReadValue<float>() > .5f ? PlayerStats.runSpeed : walkSpeed;

        Vector3 moveVec = GetMovementVector(currentSpeed);

        if (characterController.isGrounded)
        {
            //Start jump
            if (InputSystem.actions["Jump"].ReadValue<float>() > .2f)
                Jump();

            //Jump physics
            if (_yVelocity < 0)
                _yVelocity = 0;
        }
        else
        {
            _yVelocity -= Time.deltaTime / jumpTime * 2 * jumpPower;
        }

        characterController.Move(moveVec + (_yVelocity / jumpTime * Time.deltaTime * Vector3.up));
        HandleState(moveVec.magnitude != 0f);
    }

    private void LateUpdate()
    {
        if (GameManager.singleton.GAMEOVER) return;
        Vector2 moveInp = InputSystem.actions["Move"].ReadValue<Vector2>();

        if (moveInp != Vector2.zero || aiming || shooting)
        {
            Vector3 dir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;

            targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        }

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void OnAimEvent(bool aimR)
    {
        aiming = aimR;
    }

    void OnShootvent(bool aimR)
    {
        shooting = aimR;
    }

    void HandleState(bool moving)
    {

        if (moving)
        {
            if (currentSpeed == PlayerStats.runSpeed)
                movementState = MovementState.Running;
            else
                movementState = MovementState.Walking;
        }
        else
        {
            movementState = MovementState.Idle;
        }
    }

    Vector3 GetMovementVector(float currentSpeed)
    {
        Vector2 moveInp = InputSystem.actions["Move"].ReadValue<Vector2>();
        Vector3 moveDir = transform.forward * moveInp.y + transform.right * moveInp.x;

        return currentSpeed * Time.deltaTime * moveDir;
    }

    void Jump()
    {
        if (!canJump) return;
        _yVelocity = jumpPower;
    }
}
