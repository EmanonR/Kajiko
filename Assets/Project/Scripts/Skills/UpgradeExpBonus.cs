using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/ExpBonus", fileName = "ExpBonus Skill")]
public class UpgradeExpBonus : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.expBonus.ResetValues();
        description = "Exp bonus +10%";
    }

    public override void Aquire()
    {
        PlayerStats.expBonus.multiplierValue += .1f;

        base.Aquire();
    }
}