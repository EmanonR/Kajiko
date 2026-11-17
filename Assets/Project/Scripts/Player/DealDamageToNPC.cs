using UnityEngine;

public class DealDamageToNPC : MonoBehaviour
{
    public int damage;
    public int hitCap;

    int hits = 0;

    private void OnTriggerEnter(Collider other)
    {
        NPC npc = other.GetComponentInParent<NPC>();

        if (npc != null)
        {
            npc.TakeDamage(damage);
            hits++;

            if (hitCap > 0 && hits >= hitCap)
                Destroy(gameObject);
        }
    }
}
