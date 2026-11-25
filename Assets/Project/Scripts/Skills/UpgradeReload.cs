using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/Reload", fileName = "Reload Skill")]
public class UpgradeReload : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.reloadSpeed.ResetValues();
        description = "Reload speed +10%";
    }

    public override void Aquire()
    {
        PlayerStats.reloadSpeed.multiplierValue += .1f;

        base.Aquire();
    }
}