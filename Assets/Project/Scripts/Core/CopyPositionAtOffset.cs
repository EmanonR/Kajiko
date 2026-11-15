using UnityEngine;

[ExecuteAlways]
public class CopyPositionAtOffset : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position + offset;
    }
}
