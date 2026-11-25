using UnityEngine;

[ExecuteAlways]
public class CopyPositionAtOffset : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position + offset;
    }
}
