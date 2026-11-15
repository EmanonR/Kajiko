using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    public GameObject dropPrefab;
    [Range(0, 100)]
    public float dropChance = 15;

    public Entity entity;

    private void Awake()
    {
        entity = transform.GetComponent<Entity>();

        if (entity != null)
            entity.OnDeath += DropDeath;
    }

    private void OnDestroy()
    {
        if (entity != null)
            entity.OnDeath -= DropDeath;
    }

    public void DropDeath()
    {
        if (Random.Range(0, 100) < dropChance * PlayerStats.Luck)
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
    }
}
