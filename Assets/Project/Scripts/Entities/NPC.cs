using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    public Transform player;
    public float visionRange = 15;
    public float attackRange = 5;
    public float interactRange = 1;

    public static List<NPC> NpcsInScene = new();

    private void Start()
    {
        player = GameManager.Instance.player.transform;
        NpcsInScene.Add(this);
    }

    public virtual void OnDestroy()
    {
        NpcsInScene.Remove(this);
    }

    public override void TakeDamage(float incoming)
    {
        if (isDead) return;

        ScoreManager.AddScore((incoming / 2) * PlayerStats.expBonus.GetCalculated());

        base.TakeDamage(incoming);
    }

    public override void Death()
    {
        base.Death();
        ScoreManager.AddScore(maxHP / 2);
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
