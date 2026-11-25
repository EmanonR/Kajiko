using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/Pickup", fileName = "Pickup Skill")]
public class UpgradePickup : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.pickupRange.ResetValues();
        description = "Pickup range +10%";
    }

    public override void Aquire()
    {
        PlayerStats.pickupRange.multiplierValue += .1f;

        base.Aquire();
    }
}