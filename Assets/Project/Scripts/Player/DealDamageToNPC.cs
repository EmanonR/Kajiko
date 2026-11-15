using UnityEngine;

public class DealDamageToNPC : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NPC npc))
            npc.TakeDamage(damage);
    }
}
