using UnityEngine;

public class HealPickUp : Interactable
{
    public float pickupProg = 0;
    int healAmount = 10;


    private void Update()
    {
        if (!interactedWith) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= 1f)
        {
            player.GetComponent<Player>().Heal(healAmount);
            Destroy(gameObject);
            return;
        }

        pickupProg += Time.deltaTime / 1f;

        transform.position = Vector3.Lerp(transform.position, player.position, pickupProg);
    }

    public override void Interact(Player player)
    {
        base.Interact(player);
    }
}
