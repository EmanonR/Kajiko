using UnityEngine;

public class HealPickUp : PickUpAble
{
    int healAmount = 10;

    public override void OnPickUp(Player player)
    {
        player.Heal(healAmount);
        base.OnPickUp(player);
    }
}
