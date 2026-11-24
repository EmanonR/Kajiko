using UnityEngine;

public class RagnarokEmber : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position + new Vector3(0,1,0)) - transform.position;

        transform.Translate(moveSpeed * Time.deltaTime * dir.normalized);
    }
}
