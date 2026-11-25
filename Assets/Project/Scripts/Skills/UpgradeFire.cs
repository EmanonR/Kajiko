using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/Fire", fileName = "Fire Skill")]
public class UpgradeFire : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.fireDamage.ResetValues();
        description = "Fire damage +15%";
    }

    public override void Aquire()
    {
        PlayerStats.fireDamage.multiplierValue += .15f;

        base.Aquire();
    }
}