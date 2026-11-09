using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    public Transform cam;
    public Transform[] spineBones; 
    public float maxPitch = 45f;
    public float smoothing = 50f;
    public Transform weaponBone; 
    public Transform emitter; 

    float currentPitch;
    bool aiming, shooting;

    string currentAnimation;

    Vector3 emitterLocalPos;
    Quaternion emitterLocalRot;

    private void Awake()
    {
        FlameThrower.onShoot += onShoot;
        FlameThrower.onAim += onAim;
    }

    private void OnDestroy()
    {
        FlameThrower.onShoot -= onShoot;
        FlameThrower.onAim -= onAim;
    }

    private void Start()
    {
        emitterLocalPos = weaponBone.InverseTransformPoint(emitter.position);
        emitterLocalRot = Quaternion.Inverse(weaponBone.rotation) * emitter.rotation;
    }

    private void Update()
    {
        if (GameManager.Instance.GAMEOVER) return;
        if (GameManager.Instance.paused) return;

        emitter.SetPositionAndRotation(
            weaponBone.TransformPoint(emitterLocalPos),
            weaponBone.rotation * emitterLocalRot);


        switch (PlayerMovement.movementState)
        {
            case PlayerMovement.MovementState.Idle:
                PlayAnimation("Idle");
                break;
            case PlayerMovement.MovementState.Walking:
                Vector2 moveInp = KeyBinds.Instance.movementInput.ReadValue<Vector2>();

                animator.SetFloat("MoveX", moveInp.x);
                animator.SetFloat("MoveZ", moveInp.y);

                PlayAnimation("WalkBlend");
                break;
            case PlayerMovement.MovementState.Running:
                PlayAnimation("Run");
                break;
            default:
                PlayAnimation("Idle");
                break;
        }
    }

    void LateUpdate()
    {
        float target = 0f;
        if (shooting || aiming)
            target = Mathf.Clamp(Mathf.DeltaAngle(0f, cam.localEulerAngles.x), -maxPitch, maxPitch);

        currentPitch = Mathf.Lerp(currentPitch, target, 1f - Mathf.Exp(-smoothing * Time.deltaTime));

        Transform last = spineBones[spineBones.Length - 1];
        last.GetPositionAndRotation(out Vector3 prevPos, out Quaternion prevRot);

        float perBone = currentPitch / spineBones.Length;
        Quaternion rot = Quaternion.AngleAxis(perBone, transform.right);
        foreach (Transform bone in spineBones)
            bone.rotation = rot * bone.rotation;

        Quaternion delta = last.rotation * Quaternion.Inverse(prevRot);

        weaponBone.SetPositionAndRotation(
            last.position + delta * (weaponBone.position - prevPos),
            delta * weaponBone.rotation
            );
    }


    public void PlayAnimation(string clipName)
    {
        if (clipName == currentAnimation) return;

        animator.Play(clipName);
        currentAnimation = clipName;
    }

    void onShoot(bool i) { shooting = i; }
    void onAim(bool i) { aiming = i; }

}