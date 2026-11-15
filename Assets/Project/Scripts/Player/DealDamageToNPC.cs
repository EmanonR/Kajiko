using UnityEngine;

public class DealDamageToNPC : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        NPC npc = other.GetComponentInParent<NPC>();

        if (npc != null)
            npc.TakeDamage(damage);
    }
}
