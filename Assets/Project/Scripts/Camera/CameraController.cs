using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    public Transform followObject;
    public Vector2 cameraOffset;
    public float mouseSensitivity = .5f;
    public float CameraYLimit = 45f;
    public float sphereCastSize = 1f;
    public LayerMask collisionMask;
    public float farZoom = 5f, nearZoom = 2f;
    public float zoomSpeed = 1f;

    [Range(0f, 1f)]
    public float targetZoom = 0f;

    Vector3 targetOffsetPos;
    Vector3 nearPos;
    Vector3 farPos;
    Vector3 zoomPos;

    float aimZoom = 0f;

    bool aiming;

    private void Start()
    {
        FlameThrower.onAim += AimOverride;
    }

    private void OnDestroy()
    {
        FlameThrower.onAim -= AimOverride;
    }

    private void LateUpdate()
    {
        if (GameManager.singleton != null)
        {
            if (GameManager.singleton.GAMEOVER) return;
            if (GameManager.singleton.paused) return;
        }

        if (followObject == null) return;
        if (KeyBinds.Instance == null) return;

        Vector2 mouseInput = KeyBinds.Instance.mouseInput.ReadValue<Vector2>();

        //Rotate camera
        if (Application.isPlaying)
            RotateCam(mouseInput);

        //Update positions
        UpdatePositions();


        //Move to position
        transform.position = targetOffsetPos;

        //Zoom
        CameraZoom();
        transform.position = zoomPos;

        //Collision
        if (!Application.isPlaying) return;

        float distToTarget = Vector3.Distance(zoomPos, followObject.position) + sphereCastSize;

        CameraHitCast(distToTarget);

        float aimZoomSpeed = 5f;

        if (aiming)
            aimZoom = Mathf.Clamp01(aimZoom + Time.deltaTime * aimZoomSpeed);
        else
            aimZoom = Mathf.Clamp01(aimZoom - Time.deltaTime * aimZoomSpeed);

        GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 45, aimZoom);
    }

    void UpdatePositions()
    {
        targetOffsetPos = followObject.position + Vector3.up * cameraOffset.y + transform.right * cameraOffset.x;
        nearPos = targetOffsetPos + -transform.forward * nearZoom;
        farPos = targetOffsetPos + -transform.forward * farZoom;
        zoomPos = Vector3.Lerp(farPos, nearPos, targetZoom);
    }

    void RotateCam(Vector2 mouseInput)
    {
        transform.eulerAngles += new Vector3(-mouseInput.y, mouseInput.x, 0) * mouseSensitivity;
        transform.eulerAngles = new(ClampRotation(transform.eulerAngles.x), transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void CameraHitCast(float dist)
    {
        Vector3 fromPos = targetOffsetPos - transform.right * cameraOffset.x + transform.forward * sphereCastSize;
        Vector3 dir = (transform.position - fromPos).normalized;
        Physics.SphereCast(fromPos, sphereCastSize, dir, out RaycastHit hit, dist, collisionMask);

        if (hit.collider != null)
            transform.position = hit.point + (hit.normal * sphereCastSize);
    }

    void CameraZoom()
    {
        targetZoom = Mathf.Clamp(targetZoom += KeyBinds.Instance.scrollInput.ReadValue<float>() * zoomSpeed, 0, 1);
    }

    float ClampRotation(float rotation)
    {
        if (rotation < 360 - CameraYLimit && rotation > CameraYLimit)
        {

            float distL = Mathf.Abs(360 - CameraYLimit - rotation);
            float distH = Mathf.Abs(CameraYLimit - rotation);

            if (distL < distH)
                rotation = 360 - CameraYLimit;
            else
                rotation = CameraYLimit;
        }
        return rotation;
    }

    void AimOverride(bool inp)
    {
        aiming = inp;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            UpdatePositions();

        Gizmos.color = Color.yellow;
        Vector3 targetPos = targetOffsetPos - transform.right * cameraOffset.x + transform.forward * sphereCastSize;

        Gizmos.DrawWireSphere(targetPos, sphereCastSize);
        Gizmos.DrawWireSphere(farPos, sphereCastSize);

        Gizmos.DrawLine(targetPos, farPos);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(nearPos, .5f);
        Gizmos.DrawWireSphere(farPos, .5f);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(zoomPos, .25f);
    }

}