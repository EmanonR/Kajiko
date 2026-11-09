using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float destroyTime = 5f;
    public float damage;
    public float radius = .25f;
    public LayerMask hitmask;

    public Rigidbody rb;

    float timer;
    Vector3 prevPos;
    bool hasHit;


    private void Start()
    {
        rb.linearVelocity = speed * rb.transform.forward;
        prevPos = rb.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= destroyTime)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (hasHit) return;

        Vector3 currentPos = rb.position;
        Vector3 dir = currentPos - prevPos;

        if (Physics.SphereCast(prevPos, radius, dir.normalized, out RaycastHit hit, dir.magnitude,hitmask, QueryTriggerInteraction.Ignore))
        {
            HandleHit(hit.collider);
        }

        prevPos = currentPos;
    }

    void HandleHit(Collider other)
    {
        //Do actual hit logic here
        if (other.GetComponentInParent<NPC>() != null) return;

        if (other.TryGetComponent(out Player player))
            player.TakeDamage(damage);

        hasHit = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
