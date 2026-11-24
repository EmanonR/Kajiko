using System.Collections.Generic;
using UnityEngine;

public class RagnarokController : MonoBehaviour
{
    public int damageAmount = 5;
    public Transform[] SpawnPositions;
    public GameObject emberPrefab;

    float spawnTime = 4f;
    float emberSpeed = 1f;
    float targetDistance = 10f;
    float timer;
    int spawnCount;
    float currentDamage;

    private void Awake()
    {
        RagnarokSkill.OnChanged += UpdateStats;

        foreach (var item in SpawnPositions)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        RagnarokSkill.OnChanged -= UpdateStats;
    }

    public void UpdateStats(int spawnCount, float spawnTime, int distance, float flySpeed, float damageMultiplier)
    {
        foreach (var item in SpawnPositions)
        {
            item.gameObject.SetActive(true);
        }

        this.spawnCount = spawnCount;
        this.spawnTime = spawnTime;
        targetDistance = distance;
        emberSpeed = flySpeed;
        currentDamage = damageAmount * damageMultiplier * PlayerStats.fireDamage.GetCalculated();
    }
    private void LateUpdate()
    {
        if (timer >= spawnTime)
        {
            //Find closest enemies
            List<Transform> targets = new();

            foreach (var t in NPC.NpcsInScene)
            {
                if (Vector3.Distance(transform.position, t.transform.position) <= targetDistance)
                    targets.Add(t.transform);
            }

            if (targets.Count == 0)
                return;


            SpawnEmbers(spawnCount, targets);
            timer -= spawnTime;
        }

        timer += Time.deltaTime;
    }

    public void SpawnEmbers(int amount, List<Transform> targets)
    {
        if (amount <= 0) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject ember = Instantiate(emberPrefab, transform.position, Quaternion.identity);

            ember.transform.position = SpawnPositions[i % SpawnPositions.Length].position;

            ember.GetComponent<DealDamageToNPC>().damage = (int)currentDamage;
            ember.GetComponent<DealDamageToNPC>().hitCap = 1;
            ember.GetComponent<RagnarokEmber>().target = targets[i % targets.Count];
            ember.GetComponent<RagnarokEmber>().moveSpeed = emberSpeed;
        }
    }
}
