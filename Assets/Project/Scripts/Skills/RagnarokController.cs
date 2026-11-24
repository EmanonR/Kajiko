using UnityEngine;

public class RagnarokController : MonoBehaviour
{
    public int damageAmount = 5;
    public Transform[] SpawnPositions;
    public GameObject emberPrefab;

    float spawnTime = 4f;
    float emberSpeed = 1f;

    float timer;
    int spawnCount;
    float currentDamage;

    private void Awake()
    {
        //RagnarokSkill.OnChanged += SetUpEmber;
    }

    private void OnDestroy()
    {
        //RagnarokSkill.OnChanged -= SetUpEmber;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnEmbers(spawnCount, null);
            timer -= spawnTime;
        }
    }

    public void SetUpEmber(int spawnCount, float damageMultiplier, float emberSpeed)
    {
        this.spawnCount = spawnCount;
        timer = spawnTime;
        currentDamage = damageAmount * damageMultiplier * PlayerStats.fireDamage.GetCalculated();
        this.emberSpeed = emberSpeed;
    }

    public void SpawnEmbers(int amount, Transform target)
    {
        if (amount <= 0) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject ember = Instantiate(emberPrefab, transform);
            ember.transform.localPosition = SpawnPositions[i % SpawnPositions.Length].position;
            ember.GetComponent<DealDamageToNPC>().damage = (int)currentDamage;
            ember.GetComponent<DealDamageToNPC>().hitCap = 1;
            ember.GetComponent<RagnarokEmber>().target = target;
            ember.GetComponent<RagnarokEmber>().moveSpeed = emberSpeed;
        }
    }
}
