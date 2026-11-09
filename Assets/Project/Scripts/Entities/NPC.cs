using UnityEngine;

public class NPC : Entity
{
    public Transform player;
    public float visionRange = 15;
    public float attackRange = 5;
    public float interactRange = 1;
    public GameObject healingPrefab;

    public override void Death()
    {
        ScoreManager.AddScore(maxHP / 2);

        if (Random.Range(0, 100) < PlayerStats.healingDropChance)
            Instantiate(healingPrefab, transform.position, Quaternion.identity);

        base.Death();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
