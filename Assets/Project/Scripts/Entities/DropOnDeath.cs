using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    public GameObject dropPrefab;
    [Range(0, 100)]
    public float dropChange;

    public Entity entity;

    private void Start()
    {
        entity = transform.GetComponent<Entity>();

        entity.OnDeath += DropDeath;
    }

    private void OnDestroy()
    {
        entity.OnDeath -= DropDeath;
    }

    public void DropDeath()
    {
        if (Random.Range(0, 100) < dropChange * PlayerStats.Luck)
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
    }
}
