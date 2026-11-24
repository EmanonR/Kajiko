using UnityEngine;

[ExecuteAlways]
public class CopyPositionAtOffset : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 offset;
    [SerializeField] bool copyRotation;
    [SerializeField] bool excludeY;

    Quaternion initialRotation;

    private void OnEnable()
    {
        initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position + offset;

        if (copyRotation)
        {
            Vector3 targetEuler = followTarget.transform.eulerAngles;
            Quaternion noYaw = Quaternion.Euler(targetEuler.x, targetEuler.y, targetEuler.z);

            if (excludeY) 
                transform.rotation = initialRotation * noYaw;
            else
                transform.rotation = initialRotation * followTarget.transform.rotation;
        }
    }
}
