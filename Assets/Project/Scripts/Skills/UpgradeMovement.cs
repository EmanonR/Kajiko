using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/Movement", fileName = "Movement Skill")]
public class UpgradeMovement : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.moveSpeedStat.ResetValues();
        description = "Movement +10%";
    }

    public override void Aquire()
    {
        PlayerStats.moveSpeedStat.multiplierValue += .1f;

        base.Aquire();
    }
}
