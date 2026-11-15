using UnityEngine;

public class NPC : Entity
{
    public Transform player;
    public float visionRange = 15;
    public float attackRange = 5;
    public float interactRange = 1;

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    public override void TakeDamage(float incoming)
    {
        ScoreManager.AddScore(incoming / 2);

        base.TakeDamage(incoming);
    }

    public override void Death()
    {
        ScoreManager.AddScore(maxHP / 2);

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
