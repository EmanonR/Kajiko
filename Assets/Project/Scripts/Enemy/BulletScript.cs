using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 15f;
    public float destroyTime = 5f;
    public float damage;

    public Rigidbody rb;

    float timer;


    private void Start()
    {
        rb.linearVelocity = speed * rb.transform.forward;
    }

    private void Update()
    {
        if (timer >= destroyTime)
            Destroy(gameObject);

        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Do actual hit logic here

        other.TryGetComponent(out Player player);

        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
