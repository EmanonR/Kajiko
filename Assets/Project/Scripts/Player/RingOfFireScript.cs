using UnityEngine;

public class RingOfFireScript : MonoBehaviour
{
    public int damageAmount = 5; 

    public GameObject emberPrefab;

    float angle;
    float rotationSpeed;
    float collisionSize;
    int hitLimit;

    private void Awake()
    {
        RingOfFire.OnChanged += SetEmberCount;
    }

    private void OnDestroy()
    {
        RingOfFire.OnChanged -= SetEmberCount;
    }

    private void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetEmberCount(int count, int hitLimit, float distance, float speed, float collisionSize)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        rotationSpeed = speed;
        this.collisionSize = collisionSize;
        SpawnEmbers(count, distance);
    }

    public void SpawnEmbers(int amount, float distance)
    {
        if (amount <= 0) return;

        float step = 360f / amount;

        for (int i = 0; i < amount; i++)
        {
            float angle = i * step * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * distance;

            GameObject ember = Instantiate(emberPrefab, transform);
            ember.transform.localPosition = offset;
            ember.GetComponent<DealDamageToNPC>().damage = damageAmount;
            ember.GetComponent<DealDamageToNPC>().hitCap = hitLimit;
            ember.GetComponent<SphereCollider>().radius = collisionSize;
        }
    }
}
