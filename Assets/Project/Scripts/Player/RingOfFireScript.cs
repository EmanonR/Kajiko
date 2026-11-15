using UnityEngine;
using UnityEngine.UIElements;

public class RingOfFireScript : MonoBehaviour
{
    public int damageAmount = 5; 
    public float rotationSpeed = 60f;
    public int startCount = 2;
    public float emberDistance = 5f;

    public GameObject emberPrefab;

    float angle;

    private void Start()
    {
        SpawnEmbers(startCount, emberDistance);
    }

    private void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetEmberCount(int count)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        SpawnEmbers(count, emberDistance);
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
        }
    }
}
