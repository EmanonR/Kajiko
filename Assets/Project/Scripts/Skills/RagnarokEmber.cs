using UnityEngine;

public class RagnarokEmber : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;

    private void Update()
    {
        Vector3 dir = transform.position - target.position;

        transform.Translate(moveSpeed * Time.deltaTime * dir);
    }
}
